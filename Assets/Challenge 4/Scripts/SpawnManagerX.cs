using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    // Префаб врага
    public GameObject enemyPrefab;
    // Префаб усиления
    public GameObject powerupPrefab;

    // Диапазон спавна по оси X
    private float spawnRangeX = 10;
    // Минимальное значение координаты Z для спавна
    private float spawnZMin = 15;
    // Максимальное значение координаты Z для спавна
    private float spawnZMax = 25;

    // Количество врагов на сцене
    public int enemyCount;
    // Текущий номер волны
    public int waveCount = 1;

    // Ссылка на объект игрока
    public GameObject player;

    // Метод Update вызывается каждый кадр
    void Update()
    {
        // Обновляем количество врагов на сцене
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // Если врагов нет, спавним новую волну
        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
        }
    }

    // Генерация случайной позиции для спавна усилений и врагов
    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }

    // Спавн волны врагов
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // смещение для спавна усилений ближе к игроку

        // Если усилений нет, спавним усиление
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Спавним врагов в количестве, равном номеру волны
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            EnemyX ec = enemy.GetComponent<EnemyX>();
            ec.speed += waveCount * 10; // увеличиваем скорость врагов с каждой волной
        }

        waveCount++;
        ResetPlayerPosition(); // возвращаем игрока на стартовую позицию
    }

    // Возвращаем игрока на стартовую позицию перед своими воротами
    void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
