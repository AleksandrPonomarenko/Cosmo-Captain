using UnityEngine;
using UnityEngine.Playables;

public class ShipEnemy : MonoBehaviour
{
    private ShipControle Player;
    public GameObject bulletEnemyPrefab;
    float xPlayer; float yPlayer;
    float xEnemy; float yEnemy;
    float speedEnemy = 0.01f;
    int healthEnemy = 3;
    float reloadTime = 3f; float elapsedTime = 0f;
    float randomDistance, randomRadios, randomDanger;
    PlayableDirector bulletSound;
    public GameObject CrashObj; float timerCrash = 0f;
    public GameObject CrashPrefab;

    void Start()
    {
        Player = GameObject.Find("PLAYER").GetComponent<ShipControle>();
        bulletSound = GetComponent<PlayableDirector>();
        reloadTime = Player.reloadTime*2;
        randomDistance = Random.Range(2, 4f);
        randomRadios = Random.Range(0.1f, 1f);
        randomDanger = Random.Range(-1f, 1f);
        speedEnemy = Random.Range(0.01f, 0.1f);
    }

    void Update()
    {
        xPlayer = Player.transform.position.x; yPlayer = Player.transform.position.y;
        xEnemy = this.transform.position.x; yEnemy = this.transform.position.y;

        if (timerCrash < 1) {timerCrash += Time.deltaTime;} else if (timerCrash >= 1) {CrashObj.SetActive(false);} 

        // Логика перемещения
        if (xEnemy > xPlayer + randomRadios){
            transform.Translate(-speedEnemy * Time.timeScale, -0f, 0f);
        } else if (xEnemy < xPlayer - randomRadios) {
            transform.Translate(speedEnemy * Time.timeScale, 0f, 0f);
        }
        if (yPlayer > randomDanger) {
            if (yEnemy < 6) {
                transform.Translate(0f, speedEnemy * Time.timeScale, 0f);
            } 
        } else if (yPlayer <= randomDanger) {
            if (yEnemy > randomDistance) {
                transform.Translate(0f, -speedEnemy * Time.timeScale, 0f);
            }
        }
        // Логика стрельбы
        if (xEnemy < xPlayer + randomRadios && xEnemy > xPlayer - randomRadios) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > reloadTime) {
                Vector3 spawnPos = transform.position; // создание точки спауна
                spawnPos += new Vector3(0, 0, 0); // определение точки спауна
                Instantiate(bulletEnemyPrefab, spawnPos, Quaternion.identity);
                bulletSound.Play();
                elapsedTime = 0f;
            }
        }
    }
    // Жизни и урон
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet") {
            CrashObj.SetActive(true); timerCrash = 0;

            if (healthEnemy >1) {
                healthEnemy -= 1;
            } else {
                Vector3 spawnPos = this.transform.position;
                Instantiate(CrashPrefab, spawnPos, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
