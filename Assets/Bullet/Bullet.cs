using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    GameManager gameManager; // объект не публичный

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>(); // объект = объект по поиску типа GameManager
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>(); // приваивания компонента
        rigidBody.velocity = new Vector2(0f, speed); // движение снаряда
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy") {
            gameManager.AddScore(); // увеличения счёта
            Destroy(gameObject); // уничтожение снаряда
        }
        
    }
}
