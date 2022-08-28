using UnityEngine;

public class BombMover : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public GameObject Bomb; Animator anim;
    public GameObject Explosion; float elapsedTime = 0f;
    public GameObject CrashPrefab;
    AudioSource audioSource; public AudioClip clip1, clip2, clip3;
    float randomSpeed, speedRotation;
    bool Activate = false; bool nextClip2 = true; bool nextClip3 = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); audioSource.volume = 0.3f;
        audioSource.clip = clip1; audioSource.pitch = 0.1f;
        anim = Bomb.GetComponent<Animator> ();
        rigidBody = GetComponent<Rigidbody2D>();
        randomSpeed = Random.Range(-0.5f, -2f); speedRotation = randomSpeed;
        float randomSize = Random.Range(2f, 3f);
        transform.localScale = new Vector3(randomSize, randomSize, 0f);
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(0, randomSpeed * Time.timeScale);
        transform.Rotate(0f, 0f, speedRotation * Time.timeScale);

        if (Activate) {
            if (nextClip2) {
                audioSource.Stop(); audioSource.clip = clip2; audioSource.Play();
                audioSource.volume = 0.5f; audioSource.pitch = 1.8f; nextClip2 = false;
            }
            elapsedTime += Time.deltaTime;
        }
        if (elapsedTime > 3) {
            if (nextClip3) {
                audioSource.Stop(); audioSource.clip = clip3; audioSource.Play();
                audioSource.volume = 1f; audioSource.pitch = 1f; nextClip3 = false;
                speedRotation = 0f;
                Vector3 spawnPos = this.transform.position;
                Instantiate(CrashPrefab, spawnPos, Quaternion.identity);
            }
            Explosion.SetActive(true);
            Destroy(gameObject, 1);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            if (!Activate) {
                randomSpeed = 0; speedRotation = 100f;
                anim.SetBool ("Activate", true);
                Activate = true;
            }
        }
    }
}
