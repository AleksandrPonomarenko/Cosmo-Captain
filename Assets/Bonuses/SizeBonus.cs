using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeBonus : MonoBehaviour
{
    private GameObject Ship;
    GameManager gameManager;
    public float speed = -2f;
    Rigidbody2D rigidBody;
    void Start()
    {
        Ship = GameObject.Find("PLAYER");
        gameManager = GameObject.FindObjectOfType<GameManager>();

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, speed);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            Ship.transform.localScale = new Vector3(-0.5f, -0.7f, 0f);
            gameManager.SizeSpawnBonusActive();
            Destroy(gameObject);
        }
    }
}
