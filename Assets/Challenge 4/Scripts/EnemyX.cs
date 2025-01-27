using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    // Скорость движения врага
    public float speed;

    // Ссылка на объект цели игрока
    private GameObject playerGoal;
    // Компонент Rigidbody врага
    private Rigidbody enemyRb;

    // Метод Start вызывается перед первым обновлением кадра
    void Start()
    {
        // Получаем компонент Rigidbody
        enemyRb = GetComponent<Rigidbody>();
        // Находим объект "Player Goal"
        playerGoal = GameObject.Find("Player Goal");
    }

    // Метод Update вызывается каждый кадр
    void Update()
    {
        // Устанавливаем направление движения врага к цели игрока и движемся в этом направлении
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
    }

    // Метод вызывается при столкновении с другим объектом
    private void OnCollisionEnter(Collision other)
    {
        // Если враг сталкивается с целью врага или целью игрока, уничтожаем его
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }
    }
}
