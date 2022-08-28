using UnityEngine;

public class Bomb : MonoBehaviour
{
    GameObject MomBomb;
    public GameObject CrashPrefab;
    private GameManager GameM;

    void Start() 
    {
        MomBomb = transform.parent.gameObject;
        GameM = GameObject.Find("GameManger").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet") {
            Vector3 spawnPos = this.transform.position;
            Instantiate(CrashPrefab, spawnPos, Quaternion.identity);
            GameM.AddScore(); GameM.playerCollection += 40;
            GameM.collectedMenu.text = GameM.playerCollection.ToString();
            Destroy(MomBomb.gameObject);
        }
    }
}
