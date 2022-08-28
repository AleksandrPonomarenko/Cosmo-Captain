using UnityEngine;

public class Saver : MonoBehaviour
{
    public GameManager GameM;
    public ShipControle Ship;
    public MenuUi Nvgtn;

    public void SaveGame()
    {
        PlayerPrefs.SetInt("SavedScore", GameM.recordScore);
        PlayerPrefs.SetInt("SavedMoney", GameM.playerMoney);
        PlayerPrefs.SetFloat("SizeReload", GameM.SizeReload);
        PlayerPrefs.SetFloat("FiveFireReload", GameM.FiveFireReload);
        PlayerPrefs.SetFloat("DCoinReload ", GameM.DCoinReload);
        PlayerPrefs.SetFloat("speed", Ship.speed);
        PlayerPrefs.SetFloat("reloadTime", Ship.reloadTime);
        PlayerPrefs.SetInt("priceSpeed", Nvgtn.priceSpeed);
        PlayerPrefs.SetInt("priceReload", Nvgtn.priceReload);
        PlayerPrefs.SetInt("priceSize", Nvgtn.priceSize);
        PlayerPrefs.SetInt("priceFiveFire", Nvgtn.priceFiveFire);
        PlayerPrefs.SetInt("priceDCoin", Nvgtn.priceDCoin);
        
        Debug.Log("Данные сохранены");
    }

    public void Start()
    {
        if (PlayerPrefs.HasKey("SavedScore"))
        { // ЗАГРУЗКА СОХРАНЕНИЙ и отображение в игре
            GameM.playerMoney = PlayerPrefs.GetInt("SavedMoney"); GameM.moneyText.text = GameM.playerMoney.ToString();
            GameM.recordScore = PlayerPrefs.GetInt("SavedScore"); GameM.recordText.text = GameM.recordScore.ToString();
            GameM.moneyTextMenu.text = GameM.playerMoney.ToString();
            GameM.SizeReload = PlayerPrefs.GetFloat("SizeReload");
            GameM.FiveFireReload = PlayerPrefs.GetFloat("FiveFireReload");
            GameM.DCoinReload = PlayerPrefs.GetFloat("DCoinReload");
            Ship.speed = PlayerPrefs.GetFloat("speed");
            Ship.reloadTime = PlayerPrefs.GetFloat("reloadTime");
            Nvgtn.priceSpeed = PlayerPrefs.GetInt("priceSpeed");
            Nvgtn.priceReload = PlayerPrefs.GetInt("priceReload");
            Nvgtn.priceSize = PlayerPrefs.GetInt("priceSize");
            Nvgtn.priceFiveFire = PlayerPrefs.GetInt("priceFiveFire");
            Nvgtn.priceDCoin = PlayerPrefs.GetInt("priceDCoin");
            Nvgtn.PriceUpdate();
            
            Debug.Log("Данные загружены");
        }
        else
            Debug.Log("Данные отсутствуют");
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();

        GameM.recordScore = 0; GameM.moneyText.text = GameM.playerMoney.ToString();
        GameM.recordText.text = GameM.recordScore.ToString(); GameM.moneyTextMenu.text = GameM.playerMoney.ToString();
        GameM.playerMoney = 0; 
        GameM.SizeReload = 5f; 
        GameM.FiveFireReload = 5f;
        GameM.DCoinReload = 5f;
        Ship.speed = 2f;
        Ship.reloadTime = 2f;
        Nvgtn.priceSpeed = 500;
        Nvgtn.priceReload = 500;
        Nvgtn.priceSize = 200;
        Nvgtn.priceFiveFire = 300;
        Nvgtn.priceDCoin = 200;
        Nvgtn.PriceUpdate();

        Debug.Log("Данные очищены");
    }
}
