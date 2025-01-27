using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    // Минимальная координата Y, ниже которой объект будет уничтожен
    [SerializeField] private float minY = 0;

    // Метод Update вызывается каждый кадр
    void Update()
    {
        // Проверяем, если позиция объекта по оси Y меньше минимального значения
        if (transform.position.y < minY)
        {
            // Уничтожаем объект
            Destroy(gameObject);
        }
    }
}
