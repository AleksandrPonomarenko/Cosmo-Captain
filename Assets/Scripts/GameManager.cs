using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText, moneyText, moneyTextMenu, scoreTextMenu, recordText, collectionText, collectedMenu;
    public int playerScore, playerMoney, playerCollection, Comparision, recordScore;
    public bool musicBoost = false;

    public GameObject MenuObj;
    public EnemySpawn Met;
    public ShipControle Ship;
    public MenuUi Nvgtn;
    public InterstitialAds Ad; public RewardedAds rAd;
    public Saver Saver;

    public AudioSource audioSource;

    public SpawnBonus Bonuses;
    public bool SizeActive = false;
    public float SizeReload = 5f; // время действия и перезарядка бонусов в ...Reload
    private float TimerSize = 0f;

    public bool FiveFireActive = false;
    public float FiveFireReload = 5f;
    private float TimerFiveFire = 0f;
    public float PastPlayerReload = 2f;

    public bool DCoin = false;
    public float DCoinReload = 5f;
    public float TimerDCoin = 0f;

    float ratio = 1f; // коэффициент ослабления старых врагов в новых уровнях сложноси

    int recordScoreSaver;

    void LoadGame() {
    }
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 0.5f; audioSource.volume = 0.5f;
    }
    public void AddScore () {
        playerScore++;
        playerMoney+=10; playerCollection +=10;
        if (DCoin) {playerScore++; playerMoney+=10;}
        collectedMenu.text = playerCollection.ToString(); scoreText.text = playerScore.ToString();
        // Система усложнения игры
        Comparision = playerScore;
        Comparision = Comparision % 3; 
        if (Comparision == 0) {
            if (Met.Lvl < 16) {
                Met.Lvl++;
                Met.minSpawnDelay -= 0.06f * ratio; Met.maxSpawnDelay -= 0.2f * ratio;
                if (Met.Stage >= 2) { Met.maxSpawnEnemyDelay -= 2f * ratio;
                } else {Met.maxSpawnEnemyDelay -= 2f;}
                if (Met.Stage >= 3) {Met.maxSpawnBombDelay -= 1f * ratio;
                } else {Met.maxSpawnBombDelay -= 1f;}
            } else if (Met.Lvl == 16) {
                Met.Lvl = 0; Met.Stage++; ratio -= 0.1f; // уровень 0, но стадия +1 и коэф спавна старых - 0.1
                // далее спавн всех врагов на исходную
                Met.minSpawnDelay = 1f; Met.maxSpawnDelay = 3.5f;
                Met.maxSpawnEnemyDelay = 60f; Met.maxSpawnBombDelay = 30f;
            }
        }

    }
    public void PlayerDied()
    {
        if (playerCollection >= 100) { int probableAd = Random.Range(1, 4);
            if (probableAd == 2) {Ad.ShowAd();} }
        if (playerCollection > 100) {rAd.buttonShowAd.interactable = true;} // вкл кнопки 2х денег за рекламу
        Nvgtn.MenuDead.SetActive(true); Nvgtn.MenuObj.SetActive(true); Nvgtn.MenuText.SetActive(false);
        scoreTextMenu.text = playerScore.ToString(); moneyText.text = playerMoney.ToString();
        moneyTextMenu.text = playerMoney.ToString(); collectionText.text = playerCollection.ToString();
        Met.Lvl = 0; Met.minSpawnDelay = 1f; Met.maxSpawnDelay = 3.5f; // откат спавна метеора
        // откат действий бонусов
        SizeActive = false; Bonuses.SizeActive = true; Ship.transform.localScale = new Vector3(1f, -1.4f, 0f);
        FiveFireActive = false; Bonuses.FiveFireActive = true; Ship.reloadTime = PastPlayerReload; Ship.FiveFireActive = false;
        Time.timeScale = 0;
        if (playerScore > recordScore) {
            recordScore = playerScore;
            recordText.text = recordScore.ToString();
        }
        audioSource.pitch = 0.5f; audioSource.volume = 0.5f;
        Met.Lvl = 0; Met.Stage = 0; ratio = 1;
        Met.maxSpawnEnemyDelay = 60f; Met.maxSpawnBombDelay = 30f;
        Saver.SaveGame(); // сохранение данных автоматически
        // AutoSave(); 
    }
    public void SizeSpawnBonusActive ()
    {
        if (SizeActive == false) {
            Bonuses.SizeActive = false;
            SizeActive = true;
            TimerSize = 0f;
        }
    }
    public void FiveFireSpawnBonusActive ()
    {
        if (FiveFireActive == false) {
            Bonuses.FiveFireActive = false;
            FiveFireActive = true;
            TimerFiveFire = 0f;
        }
    }
    void Update ()
    {
        // таймеры бонусов, их действия и включение выключение
        if (SizeActive && TimerSize < SizeReload) {
            TimerSize += Time.deltaTime;
        } else if (TimerSize >= SizeReload) {
            SizeActive = false; Bonuses.SizeActive = true; Ship.transform.localScale = new Vector3(1f, -1.4f, 0f);
        }
        if (FiveFireActive && TimerFiveFire < FiveFireReload) {
            TimerFiveFire += Time.deltaTime;
        } else if (TimerFiveFire >= FiveFireReload) {
            FiveFireActive = false; Bonuses.FiveFireActive = true; Ship.reloadTime = PastPlayerReload; Ship.FiveFireActive = false;
        }
        if (DCoin && TimerDCoin < DCoinReload) {
            TimerDCoin +=Time.deltaTime;
        } else if (TimerDCoin >= DCoinReload) {
            DCoin = false;
        }
        if (Input.GetKey(KeyCode.Z)) {playerMoney += 1000;}
        if (Input.GetKeyDown(KeyCode.P)) {Saver.ResetData();}

        if (musicBoost) { // ускорение игры и музыки после начала игры
            if (Time.timeScale <= 1) {Time.timeScale += 0.01f;}
            audioSource.pitch += 0.1f * Time.deltaTime; audioSource.volume -= 0.07f * Time.deltaTime;
            if (audioSource.pitch >= 1f) {musicBoost = false;}
        }
    }
}
