using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    // Скорость вращения камеры
    private float speed = 200;
    // Ссылка на объект игрока
    public GameObject player;

    // Метод Update вызывается каждый кадр
    void Update()
    {
        // Получаем значение горизонтального ввода (клавиши управления)
        float horizontalInput = Input.GetAxis("Horizontal");
        // Вращаем камеру вокруг оси Y (вверх) на основе полученного ввода
        transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);

        // Перемещаем камеру вместе с игроком
        transform.position = player.transform.position;
    }
}
