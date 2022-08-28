using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed = -5f;
    private ShipControle Player;
    GameManager gameManager; // объект не публичный

    void Start()
    {
        Player = GameObject.Find("PLAYER").GetComponent<ShipControle>();
        gameManager = GameObject.FindObjectOfType<GameManager>(); // объект = объект по поиску типа GameManager
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>(); // приваивания компонента
        rigidBody.velocity = new Vector2(0f, speed * Time.timeScale); // движение снаряда
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            // Player.Damage();
            Destroy(gameObject);
        }
        
    }
}
