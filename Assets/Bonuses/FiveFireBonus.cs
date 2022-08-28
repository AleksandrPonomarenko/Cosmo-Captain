using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiveFireBonus : MonoBehaviour
{
    private ShipControle Ship;
    private GameManager gameManager;
    public float speed = -2f;
    Rigidbody2D rigidBody;
    void Start()
    {
        Ship = GameObject.Find("PLAYER").GetComponent<ShipControle>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, speed);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            // прокачка
            gameManager.PastPlayerReload = Ship.reloadTime;
            Ship.reloadTime = 0.1f;
            Ship.FiveFireActive = true;
            gameManager.FiveFireSpawnBonusActive(); // запускаем работу таймера бонуса и др.
            Destroy(gameObject);
        }
    }
}
