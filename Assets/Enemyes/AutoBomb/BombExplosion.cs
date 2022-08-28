using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    private ShipControle Player;
    void Start()
    {
        Player = GameObject.Find("PLAYER").GetComponent<ShipControle>();
    }

    void Update()
    {
    
    }
    void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player") {
                Player.Damage();
            }
            if (other.tag == "Enemy") {
                Destroy(other.gameObject);
            }
        }
}
