using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Префаб врага, который будет спавниться
    [SerializeField] private GameObject enemyPrefab = null;
    // Префаб усиления, которое будет спавниться
    [SerializeField] private GameObject powerupPrefab = null;

    // Максимальный радиус спавна
    [SerializeField] private float maxSpawnRadius = 0;
    // Минимальное расстояние от игрока, на котором могут спавниться объекты
    [SerializeField] private float minDistanceFromPlayer = 0;

    // Интервал спавна
    [SerializeField] private float spawnInterval = 0;

    // Ссылка на объект игрока
    private GameObject player;
    // Текущая волна врагов
    private int wave = 0;

    // Метод Start вызывается перед первым обновлением кадра
    void Start()
    {
        // Находим объект игрока по имени
        player = GameObject.Find("Player");
    }

    // Метод Update вызывается каждый кадр
    void Update()
    {
        // Если нет врагов на сцене
        if (GameObject.FindObjectsOfType<EnemyController>().Length <= 0)
        {
            // Увеличиваем номер волны
            wave++;

            // Спавним врагов в количестве, равном номеру волны
            for (int i = 0; i < wave; i++)
            {
                SpawnEnemy();
            }

            // Спавним усиление
            SpawnPowerup();
        }
    }

    // Метод для спавна врага
    private void SpawnEnemy()
    {
        if (enemyPrefab && player)
        {
            // Получаем позицию для спавна
            Vector3 spawnPos = GetSpawnPos();
            // Создаем экземпляр врага
            GameObject.Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
        }
    }

    // Метод для спавна усиления
    private void SpawnPowerup()
    {
        if (powerupPrefab && player)
        {
            // Получаем позицию для спавна
            Vector3 spawnPos = GetSpawnPos();
            // Создаем экземпляр усиления
            GameObject.Instantiate(powerupPrefab, spawnPos, enemyPrefab.transform.rotation);
        }
    }

    // Метод для получения позиции спавна
    private Vector3 GetSpawnPos()
    {
        while (true)
        {
            // Генерируем случайную позицию в пределах максимального радиуса спавна
            Vector3 spawnPos = Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward * Random.Range(0, maxSpawnRadius);

            // Проверяем, что позиция находится на достаточном расстоянии от игрока
            if (Vector3.Distance(player.transform.position, spawnPos) >= minDistanceFromPlayer)
            {
                return spawnPos;
            }
        }
    }
}
