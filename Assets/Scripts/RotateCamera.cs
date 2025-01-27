using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // Скорость вращения камеры при использовании клавиш управления
    [SerializeField] private float axisRotationSpeed = 0;
    // Скорость вращения камеры при использовании мыши
    [SerializeField] private float mouseRotationSpeed = 0;

    // Метод Update вызывается каждый кадр
    void Update()
    {
        // Получаем значение горизонтального ввода (клавиши управления)
        float hInput = Input.GetAxis("Horizontal") * axisRotationSpeed;
        // Получаем значение ввода мыши по оси X
        float mInput = Input.GetAxis("Mouse X") * mouseRotationSpeed;

        // Используем ввод от клавиш управления, если он не равен нулю, иначе используем ввод от мыши
        float input = hInput != 0 ? hInput : mInput;

        // Вращаем камеру вокруг оси Y (вверх) на основе полученного ввода
        transform.Rotate(Vector3.up * input * Time.deltaTime);
    }
}
