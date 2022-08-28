using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUi : MonoBehaviour
{
    public Button ButtStart, ButtExit, ButtUpgrade, ButtSpeed, ButtReload, ButtSize, ButtFiveFire, ButtDCoin, ButtThanks;

    public GameObject MenuText, MenuObj, MenuUpgrade, MenuDead, MenuSetting, MenuPause;
    public GameObject Health1, Health2, Health3;
    public Text TextPriceSpeed;
    public Text TextPriceReload;
    public Text TextPriceSize;
    public Text TextPriceFiveFire;
    public Text TextPriceDCoin;

    public SpawnBonus Bonuses;
    public ShipControle Ship;
    public GameManager GameM;
    public InterstitialAds Ad;
    public RewardedAds rAd;

    public int priceSpeed = 500;
    public int priceReload = 500;
    public int priceSize = 200;
    public int priceFiveFire = 300;
    public int priceDCoin = 200;

    int percent; bool Pause = false;

	void Start () {
        rAd.LoadAd();
    
		Button btnStart = ButtStart.GetComponent<Button>();
		btnStart.onClick.AddListener(ClickStart);
        Button btnExit = ButtExit.GetComponent<Button>();
		btnExit.onClick.AddListener(ClickExit);
        Button btnUpgrade = ButtUpgrade.GetComponent<Button>();
        btnUpgrade.onClick.AddListener(ClickUpdate);
        Button btnSpeed = ButtSpeed.GetComponent<Button>();
        btnSpeed.onClick.AddListener(ClickSpeed);
        Button btnReload = ButtReload.GetComponent<Button>();
        btnReload.onClick.AddListener(ClickReload);
        Button btnSize = ButtSize.GetComponent<Button>();
        btnSize.onClick.AddListener(ClickSize);
        Button btnFiveFire = ButtFiveFire.GetComponent<Button>();
        btnFiveFire.onClick.AddListener(ClickFiveFire);
        Button BtnDCoin = ButtDCoin.GetComponent<Button>();
        BtnDCoin.onClick.AddListener(ClickDCoin);

        TextPriceSpeed.text = priceSpeed.ToString();
        TextPriceReload.text = priceReload.ToString();
        TextPriceSize.text = priceSize.ToString();
        TextPriceFiveFire.text = priceFiveFire.ToString();

        Time.timeScale = 0; // начинаем игру остановленной
	}
	public void ClickStart() {
        if(!Pause) {
            GameM.playerScore = 0; GameM.playerCollection = 0;
        }
        MenuObj.SetActive(false); MenuUpgrade.SetActive(false); MenuSetting.SetActive(false); MenuPause.SetActive(false);
        GameM.collectedMenu.text = GameM.playerCollection.ToString(); GameM.scoreText.text = GameM.playerScore.ToString();
        GameM.musicBoost = true; Pause = false;
	}
    public void ClickPause() {
        Time.timeScale = 0; GameM.audioSource.pitch = 0.5f; GameM.audioSource.volume = 0.5f;
        Pause = true; MenuPause.SetActive(true); GameM.musicBoost = false;
    }
    void ClickExit() {
        Application.Quit();
    }
    public void ClickSetting() {
        MenuText.SetActive(false);
        MenuSetting.SetActive(true);
    }
    void ClickUpdate() {
        MenuText.SetActive(false);
        MenuUpgrade.SetActive(true);
        PriceUpdate();
    }
    void ClickSpeed() { // Прокачка
        GameM.playerMoney = GameM.playerMoney - priceSpeed; // Вычитание бюджета
        Ship.speed = Ship.speed + Ship.speed * 0.05f; // Сама прокачка
        percent = priceSpeed / 20; priceSpeed = priceSpeed + percent; // Новый ценник
        TextPriceSpeed.text = priceSpeed.ToString(); // Отображение цены
        PriceUpdate();
    }
    void ClickReload() {
        GameM.playerMoney = GameM.playerMoney - priceSpeed;
        Ship.reloadTime = Ship.reloadTime - Ship.reloadTime * 0.05f;
        percent = priceReload / 20; priceReload = priceReload + percent;
        TextPriceReload.text = priceReload.ToString();
        PriceUpdate();
        if (GameM.playerMoney >= priceReload && Ship.reloadTime > 0.3f) {ButtReload.interactable = true;
        } else {ButtReload.interactable = false;}
    }
    void ClickSize() {
        GameM.playerMoney = GameM.playerMoney - priceSize;
        Bonuses.minSpawnSize = Bonuses.minSpawnSize - 2f; Bonuses.maxSpawnSize = Bonuses.maxSpawnSize - 3f;
        GameM.SizeReload = GameM.SizeReload + 0.5f;
        percent = priceSize / 20; priceSize = priceSize + percent;
        TextPriceSize.text = priceSize.ToString();
        PriceUpdate();
        if (GameM.playerMoney >= priceSize && Bonuses.minSpawnSize > 10f) {ButtSize.interactable = true;
        } else {ButtSize.interactable = false;}
    }
    void ClickFiveFire() {
        GameM.playerMoney = GameM.playerMoney - priceSize;
        Bonuses.minSpawnFiveFire=Bonuses.minSpawnFiveFire-2f; Bonuses.maxSpawnFiveFire=Bonuses.maxSpawnFiveFire-3f;
        GameM.FiveFireReload = GameM.FiveFireReload + 0.5f;
        percent = priceFiveFire / 20; priceFiveFire = priceFiveFire + percent;
        TextPriceFiveFire.text = priceFiveFire.ToString();
        PriceUpdate();
        if (GameM.playerMoney >= priceFiveFire && Bonuses.minSpawnFiveFire > 10f) {ButtFiveFire.interactable = true;
        } else {ButtFiveFire.interactable = false;}
    }
    void ClickDCoin() {
        GameM.playerMoney = GameM.playerMoney - priceDCoin;
        Bonuses.minSpawnDCoin=Bonuses.minSpawnDCoin-2f; Bonuses.maxSpawnDCoin=Bonuses.maxSpawnDCoin-3f;
        GameM.DCoinReload = GameM.DCoinReload + 0.5f;
        percent = priceDCoin / 20; priceDCoin = priceDCoin + percent;
        TextPriceDCoin.text = priceDCoin.ToString();
        PriceUpdate();
        if (GameM.playerMoney >= priceDCoin && Bonuses.minSpawnDCoin > 10f) {ButtDCoin.interactable = true;
        } else {ButtDCoin.interactable = false;}
    }
    void Update () {
        // GameM.moneyText.text = GameM.playerMoney.ToString();
    }
    public void PriceUpdate() {
        // если хватает денег и прокачка не максимальна, то прокачка работает
        if (GameM.playerMoney >= priceSpeed && Ship.speed < 10f) {ButtSpeed.interactable = true;
        } else {ButtSpeed.interactable = false;} // иначе нет
        if (GameM.playerMoney >= priceReload && Ship.reloadTime > 0.1f) {ButtReload.interactable = true;
        } else {ButtReload.interactable = false;}
        if (GameM.playerMoney >= priceSize && Bonuses.minSpawnSize > 30f) {ButtSize.interactable = true;
        } else {ButtSize.interactable = false;}
        if (GameM.playerMoney >= priceFiveFire && Bonuses.minSpawnFiveFire > 30f) {ButtFiveFire.interactable = true;
        } else {ButtFiveFire.interactable = false;}
        if (GameM.playerMoney >= priceDCoin && Bonuses.minSpawnDCoin > 30f) {ButtDCoin.interactable = true;
        } else {ButtDCoin.interactable = false;}
        TextPriceSpeed.text = priceSpeed.ToString(); TextPriceReload.text = priceReload.ToString();
        TextPriceSize.text = priceSize.ToString(); TextPriceFiveFire.text = priceFiveFire.ToString();
        TextPriceDCoin.text = priceDCoin.ToString();
    }
    public void Thanks() {
        MenuText.SetActive(true); MenuDead.SetActive(false);
    }
}
