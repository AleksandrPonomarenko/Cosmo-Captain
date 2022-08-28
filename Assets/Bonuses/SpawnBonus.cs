using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    public float spawnXLimit = 3f; // разброс точки спавна по X
    // спавн бонуса размера
    public GameObject SizePrefab;
    public bool SizeActive = true;
    public float minSpawnSize = 60f;
    public float maxSpawnSize = 180f;
     // спавн бонуса пулемета
    public GameObject FiveFirePrefab;
    public bool FiveFireActive = true;
    public float minSpawnFiveFire = 60f;
    public float maxSpawnFiveFire = 180f;
    // спавн бонуса двойного опыта
    public GameObject DCoinPrefab;
    public float minSpawnDCoin = 60f;
    public float maxSpawnDCoin = 180f;

    void Start()
    {
        SpawnSize();
        SpawnFiveFire();
        SpawnDCoin();
    }
    void SpawnSize ()
    {   
        if (SizeActive) {
            float randomSize = Random.Range(-spawnXLimit, spawnXLimit);
            Vector3 spawnPos = transform.position + new Vector3(randomSize, 0f, 0f);
            Instantiate(SizePrefab, spawnPos, Quaternion.identity);
        }
        Invoke("SpawnSize", Random.Range(minSpawnSize, maxSpawnSize)); 
    }
    void SpawnFiveFire()
    {
        if (FiveFireActive) {
            float randomFiveFire = Random.Range(-spawnXLimit, spawnXLimit);
            Vector3 spawnPos = transform.position + new Vector3(randomFiveFire, 0f, 0f);
            Instantiate(FiveFirePrefab, spawnPos, Quaternion.identity);
        }
        Invoke("SpawnFiveFire", Random.Range(minSpawnFiveFire, maxSpawnFiveFire));
    }
    void SpawnDCoin() {
        float randomDCoin = Random.Range(-spawnXLimit, spawnXLimit);
        Vector3 spawnPos = transform.position + new Vector3(randomDCoin, 0f, 0f);
        Instantiate(DCoinPrefab, spawnPos, Quaternion.identity);

        Invoke("SpawnDCoin", Random.Range(minSpawnDCoin, maxSpawnDCoin));
    }
}
