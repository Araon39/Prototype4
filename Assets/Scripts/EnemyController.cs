using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Скорость движения врага
    [SerializeField] private float velocity = 0;

    // Компонент Rigidbody врага
    private Rigidbody rb = null;
    // Ссылка на объект игрока
    private GameObject player = null;

    // Метод Start вызывается перед первым обновлением кадра
    void Start()
    {
        // Получаем компонент Rigidbody
        rb = GetComponent<Rigidbody>();
        // Находим объект игрока по имени
        player = GameObject.Find("Player");
    }

    // Метод FixedUpdate вызывается с фиксированными интервалами (используется для физики)
    void FixedUpdate()
    {
        // Если компонент Rigidbody и объект игрока существуют
        if (rb && player)
        {
            // Добавляем силу для движения врага в направлении игрока
            rb.AddForce((player.transform.position - transform.position).normalized * velocity * Time.deltaTime);
        }
    }
}
