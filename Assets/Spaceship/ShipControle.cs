using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class ShipControle : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject bulletPrefab;
    public MenuUi Navigation;
    public Joystick joystick; public Text textInfoControl; private GameObject joystickObj;
    public float speed = 5f;
    public float xLimit = 2.5f;
    public float yLimit = 3f;
    public float reloadTime = 2f;
    public Button ButtShoot; private GameObject ShootObj;
    public bool FiveFireActive = false;
    public int Health = 3;
    public GameObject Blue;
    public bool Immortality = false;
    private float DefDeadTime = 5f;
    private float elapsedTime = 0f;
    private int inputControl = 0;
    PlayableDirector bulletSound;
    public GameObject CrashObj; float timerCrash = 0f;
    


    void Start () 
    {
        Button btnShoot = ButtShoot.GetComponent<Button>();
		btnShoot.onClick.AddListener(Fire);

        bulletSound = GetComponent<PlayableDirector>();
        joystickObj = GameObject.Find("Dynamic Joystick");
        ShootObj = GameObject.Find("Btn shoot");
    }
    void Update()
    {
        elapsedTime += Time.deltaTime; // отсчёт времени после выстрела

        if (timerCrash < 1) {timerCrash += Time.deltaTime;} else if (timerCrash >= 1) {CrashObj.SetActive(false);} 

        // ПЕРЕМЕЩЕНИЕ ИГРОКА
        if (inputControl == 0) { // Управлением стиком
            float xInput = joystick.Horizontal; transform.Translate(xInput*speed*Time.deltaTime, 0f, 0f);
            float yInput = joystick.Vertical; transform.Translate(0f, yInput*speed*Time.deltaTime, 0f);
        } else if (inputControl == 1) { // Управление акссилирометром
            float xInput = Input.acceleration.x*2;
            float yInput = Input.acceleration.z*1.5f;
            transform.Translate(xInput*speed*Time.deltaTime, -yInput*speed*Time.deltaTime, 0f);
        } else { // Управление с клавиатуры
            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");
            transform.Translate(xInput*speed*Time.deltaTime, yInput*speed*Time.deltaTime, 0f);
        }

        // фиксируем положение корабля по осям
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -xLimit, xLimit); // орграничение пермещения
        position.y = Mathf.Clamp(position.y, -yLimit, yLimit);
        transform.position = position;

        // если нажат прыжок (пробел) и таймер перезарядки прошёл
        if (Input.GetButtonDown("Jump") && elapsedTime > reloadTime)
        {
            Fire();
        }

        if (elapsedTime > reloadTime) { ButtShoot.interactable = true;} else { ButtShoot.interactable = false;}
        if (DefDeadTime < 5f) {Blue.SetActive(true); DefDeadTime += Time.deltaTime; Immortality = true;}
        else {Blue.SetActive(false); Immortality = false;}
    }
    public void Fire() {
        // создание экземпляра снаряда перед игроком
            Vector3 spawnPos = transform.position; // создание точки спауна
            spawnPos += new Vector3(0, 1.2f, 0); // определение точки спауна
            Instantiate(bulletPrefab, spawnPos, Quaternion.identity); // ???
            bulletSound.Play();
                
            if (FiveFireActive) { // Бонус пулемёта
                Vector3 spawnPosFF1 = transform.position; spawnPosFF1 += new Vector3(0.2f, 1f, 0);
                Instantiate(bulletPrefab, spawnPosFF1, Quaternion.identity);
                Vector3 spawnPosFF2 = transform.position; spawnPosFF2 += new Vector3(-0.2f, 1f, 0);
                Instantiate(bulletPrefab, spawnPosFF2, Quaternion.identity);
                Vector3 spawnPosFF3 = transform.position; spawnPosFF3 += new Vector3(0.4f, 0.8f, 0);
                Instantiate(bulletPrefab, spawnPosFF3, Quaternion.identity);
                Vector3 spawnPosFF4 = transform.position; spawnPosFF4 += new Vector3(-0.4f, 0.8f, 0);
                Instantiate(bulletPrefab, spawnPosFF4, Quaternion.identity);
            }
            elapsedTime = 0f; // таймер перезарядки на ноль
    }
    // столкновение метеора с игроком
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && !Immortality) {
            Damage(); CrashObj.SetActive(true); timerCrash = 0;
        }  
    }
    public void Damage () {
        if (Health == 3) {
                Navigation.Health3.SetActive(false); Health = Health -1;
            } else if (Health == 2) {
                Navigation.Health2.SetActive(false); Health = Health -1;
            } else {  gameManager.PlayerDied(); DefDeadTime = 0f;
                Navigation.Health3.SetActive(true); Navigation.Health2.SetActive(true); Health = 3;} 
    }
    public void ChangeInput () {
        if (inputControl == 0) { joystickObj.SetActive(false);
            inputControl++; textInfoControl.text = "Accelerometer control: tilt your device";
        } else if (inputControl == 1) {
            inputControl++; textInfoControl.text = "Keyboard control: use 'Space' and 'WASD' or 'arrows'"; ShootObj.SetActive(false);
        } else {inputControl = 0; textInfoControl.text = "Joystick control: use the joystick on the screen"; joystickObj.SetActive(true);
        ShootObj.SetActive(true); }
    }
}
