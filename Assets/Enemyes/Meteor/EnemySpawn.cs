using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject meteorPrefab; // ссылка на префаб
    public GameObject EnemyPrefab;
    public GameObject BombPrefab;
    public float minSpawnDelay = 1f; // минимальное время на новый спавн
    public float maxSpawnDelay = 3.5f; // максимальное
    public float minSpawnEnemyDelay = 12f, maxSpawnEnemyDelay = 60f;
    public float minSpawnBombDelay = 5f, maxSpawnBombDelay = 30f;
    public float spawnXLimit = 2.5f; // разброс точки спавна по X
    public int Lvl = 0, Stage = 0;
    void Start()
    {
        Spawn();
        SpawnEnemy();
        SpawnBomb();
    }
    void Spawn ()
    {
        // метеор создается в случайной позиции по X
        float random = Random.Range(-spawnXLimit, spawnXLimit); // создание рандомной точки в предалах нашей переменной
        Vector3 spawnPos = transform.position + new Vector3(random, 0f, 0f); // позиция нового метеора из рандома
        Instantiate(meteorPrefab, spawnPos, Quaternion.identity); // вращение по умолчанию?

        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay)); 
        // повторяет функцию Спавн через рандомное время в пределах наших переменных
    }
    void SpawnEnemy()
    {
        if (Stage >= 1) {
            float random = Random.Range(-spawnXLimit, spawnXLimit);
            Vector3 spawnPosE = transform.position + new Vector3(random, 0f, 0f);
            Instantiate(EnemyPrefab, spawnPosE, Quaternion.identity);
        }
        Invoke("SpawnEnemy", Random.Range(minSpawnEnemyDelay, maxSpawnEnemyDelay)); 
    }
    void SpawnBomb ()
    {
        if (Stage >= 2) {
            float random = Random.Range(-spawnXLimit, spawnXLimit);
            Vector3 spawnPosB = transform.position + new Vector3(random, 0f, 0f);
            Instantiate(BombPrefab, spawnPosB, Quaternion.identity);
        }
        Invoke("SpawnBomb", Random.Range(minSpawnBombDelay, maxSpawnBombDelay));
    }
    public void Crash () {
        // Instantiate(CrashPrefab, transform.position, transform.rotation);
    }
}
