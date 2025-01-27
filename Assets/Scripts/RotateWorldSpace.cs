using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorldSpace : MonoBehaviour
{
    // Скорость вращения объекта в мировом пространстве
    [SerializeField] private Vector3 rotationSpeed = new Vector3();

    // Метод Update вызывается каждый кадр
    void Update()
    {
        // Вращаем объект вокруг осей X, Y и Z в мировом пространстве
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
    }
}
