using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    // Компонент Rigidbody игрока
    private Rigidbody playerRb;
    // Скорость движения игрока
    private float speed = 500;
    // Сила ускорения
    private float boostPower = 1;
    // Ссылка на объект, который определяет направление движения
    private GameObject focalPoint;
    // Система частиц для отображения эффекта ускорения
    private ParticleSystem particleSystem;

    // Флаг наличия усиления
    public bool hasPowerup;
    // Индикатор усиления
    public GameObject powerupIndicator;
    // Длительность действия усиления
    public int powerUpDuration = 5;

    // Сила удара по врагу без усиления
    private float normalStrength = 10;
    // Сила удара по врагу с усилением
    private float powerupStrength = 25;

    // Метод Start вызывается перед первым обновлением кадра
    void Start()
    {
        // Получаем компонент Rigidbody
        playerRb = GetComponent<Rigidbody>();
        // Получаем систему частиц
        particleSystem = GetComponentInChildren<ParticleSystem>();
        // Находим объект "Focal Point"
        focalPoint = GameObject.Find("Focal Point");
    }

    // Метод Update вызывается каждый кадр
    void Update()
    {
        // Получаем значение вертикального ввода (клавиши управления)
        float verticalInput = Input.GetAxis("Vertical");
        // Получаем значение ввода для ускорения
        bool boostInput = Input.GetButton("Boost");
        // Добавляем силу для движения игрока в направлении focalPoint
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);

        // Если нажата кнопка ускорения, добавляем силу ускорения и запускаем систему частиц
        if (boostInput)
        {
            playerRb.AddForce(focalPoint.transform.forward * boostPower, ForceMode.Impulse);
            particleSystem.Play();
        }

        // Устанавливаем позицию индикатора усиления под игроком
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
    }

    // Метод вызывается при входе в триггер
    private void OnTriggerEnter(Collider other)
    {
        // Если игрок собирает усиление
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);

            // Запускаем корутину для отсчета времени действия усиления
            StartCoroutine(PowerupCooldown());
        }
    }

    // Корутин для отсчета времени действия усиления
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // Метод вызывается при столкновении с другим объектом
    private void OnCollisionEnter(Collision other)
    {
        // Если игрок сталкивается с врагом
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

            // Если у игрока есть усиление, отталкиваем врага с большей силой
            if (hasPowerup)
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            // Если усиления нет, отталкиваем врага с обычной силой
            else
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }
}
