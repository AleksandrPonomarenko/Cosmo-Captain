using UnityEngine;

public class MeteorMover : MonoBehaviour
{
    EnemySpawn Met;
    Rigidbody2D rigidBody;
    public GameObject CrashPrefab; float xPos, yPos; 

    float minSpawnSize = 0.5f; float maxSpawnSize = 1f;
    float minSpawnSpeed = -1f; float maxSpawnSpeed = -3f;
    void Start()
    {
        Met = GameObject.Find("Meteor Spawn").GetComponent<EnemySpawn>();
        rigidBody = GetComponent<Rigidbody2D>();

        float randomSize = Random.Range(minSpawnSize, maxSpawnSize);
        transform.localScale = new Vector3(randomSize, randomSize, 0f);
        
        float randomSpeed = Random.Range(minSpawnSpeed, maxSpawnSpeed);

        rigidBody.velocity = new Vector2(0, randomSpeed);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" || other.tag == "Player") {
            Vector3 spawnPos = this.transform.position;
            Instantiate(CrashPrefab, spawnPos, Quaternion.identity);
            Destroy(gameObject); // уничтожение метеора
        }
    }
}
