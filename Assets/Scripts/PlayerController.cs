using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Скорость движения игрока
    [SerializeField] private float velocity = 0;
    // Ссылка на объект, который определяет направление движения
    [SerializeField] private GameObject focalPoint = null;
    // Ссылка на индикатор усиления
    [SerializeField] private GameObject powerupIndicator = null;
    // Сила, с которой игрок отталкивает врагов при наличии усиления
    [SerializeField] private float powerupForce = 0;
    // Длительность действия усиления
    [SerializeField] private float powerupDuration = 0;

    // Компонент Rigidbody игрока
    private Rigidbody rb;
    // Флаг наличия усиления
    private bool hasPowerup = false;

    // Метод Start вызывается перед первым обновлением кадра
    void Start()
    {
        // Получаем компонент Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    // Метод Update вызывается каждый кадр
    void Update()
    {
        // Обновляем позицию и активность индикатора усиления
        if (powerupIndicator)
        {
            powerupIndicator.transform.position = transform.position;
            powerupIndicator.SetActive(hasPowerup);
        }
    }

    // Метод FixedUpdate вызывается с фиксированными интервалами (используется для физики)
    void FixedUpdate()
    {
        // Получаем значение вертикального ввода (клавиши управления)
        float vInput = Input.GetAxis("Vertical");

        // Добавляем силу для движения игрока в направлении focalPoint
        if (focalPoint)
        {
            rb.AddForce(focalPoint.transform.forward * velocity * vInput * Time.deltaTime);
        }
    }

    // Метод вызывается при входе в триггер
    void OnTriggerEnter(Collider other)
    {
        // Если игрок собирает усиление
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;

            // Запускаем корутину для отсчета времени действия усиления
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    // Метод вызывается при столкновении с другим объектом
    void OnCollisionEnter(Collision collision)
    {
        // Если игрок сталкивается с врагом и у него есть усиление
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            if (enemyRb)
            {
                // Отталкиваем врага от игрока
                Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;
                enemyRb.AddForce(awayFromPlayer * powerupForce, ForceMode.Impulse);
            }
        }
    }

    // Корутин для отсчета времени действия усиления
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        hasPowerup = false;
    }
}
