using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuLogic : MonoBehaviour
{
    [Header("Botones interfaz")]
    public Button enterGameButton;
    public Button creditButton;
    public Button backButton;
    public Button level1Button;
    public Button level2Button;
    public Button storeButton;
    
    [Header ("Botones de la tienda")]
    public Button coinsButton;
    public Button gemsButton;
    public Button timeButton;
    public Button assetsButton;
    [Header("Textos de la tienda")]
    public Text text1;
    public Text text2;
    public Text text3;

    [Header("Componentes")]
    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject creditsUI;
    public GameObject storeUI;

    [Header("Información del jugador")]
    public Text coinsText;
    public Text gemsText;
    public Text timeText;
    private int playerCoins;
    private int playerGems;
    private float totalTime;

    #region Methods
    void Start()
    {
        playerCoins = GameManager.instance.playerCoins;
        playerGems = GameManager.instance.playerGems;
        totalTime = GameManager.instance.playerTimeLeft;
        PlayerDataOut();
    }
    public void OnEnterGameButton()
    {
        menuUI.SetActive(false);
        creditsUI.SetActive(false);
        storeUI.SetActive(false);
        gameUI.SetActive(true);

    }
    public void OnStoreGameButton()
    {
        menuUI.SetActive(false);
        creditsUI.SetActive(false);
        storeUI.SetActive(true);
        gameUI.SetActive(false);
        OnCoinsStoreButton();

    }
    public void OnCreditsGameButton()
    {
        menuUI.SetActive(false);
        creditsUI.SetActive(true);
        storeUI.SetActive(false);
        gameUI.SetActive(false);

    }
    public void OnLevelGameButton( int level)
    {
        switch (level)
        {
            case 0:
                //cambio a pantalla de juego nivel 1
                GameManager.instance.FadeToLevel(1);
                break;
            case 1:
                //cambio a pantalla de juego nivel 2;
                break;
        }

    }
    public void OnBackButton(int index)
    {
        switch (index)
        {
            case 0:
                menuUI.SetActive(true);
                creditsUI.SetActive(false);
                storeUI.SetActive(false);
                gameUI.SetActive(false);
                break;
            case 1:
                menuUI.SetActive(true);
                creditsUI.SetActive(false);
                storeUI.SetActive(false);
                gameUI.SetActive(false);
                break;
            case 2:
                menuUI.SetActive(false);
                creditsUI.SetActive(false);
                storeUI.SetActive(false);
                gameUI.SetActive(true);
                break;
        }

    }
    public void PlayerDataOut()
    {

        coinsText.text = playerCoins.ToString();
        gemsText.text = playerGems.ToString();
        timeText.text = totalTime.ToString();

    }

    #region Gems
    public void OnGemsStoreButton()
    {
        gemsButton.GetComponentInChildren<Canvas>().enabled = true;
        coinsButton.GetComponentInChildren<Canvas>().enabled = false;
        timeButton.GetComponentInChildren<Canvas>().enabled = false;
        assetsButton.GetComponentInChildren<Canvas>().enabled = false;

        text1.text = "1  euro + 100 monedas";
        text2.text = "3  euros + 200 monedas";
        text3.text = "5  euros + 350 monedas";
    }
    public void SelectGemsBuy(int i)
    {
        switch (i)
        {
            case 0:
                BuyGems(10, 1, 100);
                break;
            case 1:
                BuyGems(30, 3, 200);
                break;
            case 2:
                BuyGems(50, 5, 350);
                break;
        }
    }
    public void BuyGems(int moreGems, int money, int coins)
    {
        Debug.Log("Has comprado" + moreGems + "monedas y me ha costado: " + money + "y" + coins + "monedas");
        ShopManager.instance.IncrementPlayerGems(moreGems);
        ShopManager.instance.IncrementPlayerCoins(-coins);
        ShopManager.instance.IncrementPlayerMoney(-money);
    }
    #endregion
    #region Coins
    public void OnCoinsStoreButton()
    {
        gemsButton.GetComponentInChildren<Canvas>().enabled = false;
        coinsButton.GetComponentInChildren<Canvas>().enabled = true;
        timeButton.GetComponentInChildren<Canvas>().enabled = false;
        assetsButton.GetComponentInChildren<Canvas>().enabled = false;
        text1.text = "1  euro ";
        text2.text = "3  euros";
        text3.text = "5  euros";
    }
    public void SelectCoinsBuy(int i)
    {
        switch (i)
        {
            case 0:
                BuyCoins(100, 1);
                break;
            case 1:
                BuyCoins(400, 3);
                break;
            case 2:
                BuyCoins(700, 5);
                break;
        }
    }
    public void BuyCoins(int moreCoins, int money)
    {
        Debug.Log("Has comprado" + moreCoins + "monedas" + "me ha costado" + money);
        ShopManager.instance.IncrementPlayerCoins(moreCoins);
        ShopManager.instance.IncrementPlayerMoney(-money);
    }
    #endregion
    #region Time
    public void OnTimeStoreButton()
    {
        gemsButton.GetComponentInChildren<Canvas>().enabled = false;
        coinsButton.GetComponentInChildren<Canvas>().enabled = false;
        timeButton.GetComponentInChildren<Canvas>().enabled = true;
        assetsButton.GetComponentInChildren<Canvas>().enabled = false;
        text1.text = "5 gemas ";
        text2.text = "8 gemas";
        text3.text = "10 gemas";
    }
    public void SelectTimeBuy(int i)
    {
        switch (i)
        {
            case 0:
                BuyTime(10,  3);
                break;
            case 1:
                BuyTime(30,  5);
                break;
            case 2:
                BuyTime(60,  7);
                break;
        }
    }
    public void BuyTime(int moreTime,  int gems)
    {
        Debug.Log("Has comprado" + moreTime + "monedas y me ha costado: " + gems + "gemas");
        ShopManager.instance.IncrementTime(moreTime);
        ShopManager.instance.IncrementPlayerCoins(-gems);
    }
    #endregion
    #region Assets
    public void OnAssetsStoreButton()
    {
        gemsButton.GetComponentInChildren<Canvas>().enabled = false;
        coinsButton.GetComponentInChildren<Canvas>().enabled = false;
        timeButton.GetComponentInChildren<Canvas>().enabled = false;
        assetsButton.GetComponentInChildren<Canvas>().enabled = true;
        text1.text = "3 euros ";
        text2.text = "5 euros";
        text3.text = "Próximamente";
    }
    public void SelectAssetsBuy(int i)
    {

        switch (i)
        {
            case 0:
                BuyCharacter(0, 3);
                break;
            case 1:
                BuyMonuments(0, 5);
                break;
            case 2:
                Debug.Log("Proximamente");
                break;

        }
    }
    public void BuyMonuments(int monument, int money)
    {
        Debug.Log("Has comprado el monumento de indice:" + monument +  "me ha costado" + money);
        ShopManager.instance.IncrementPlayerAssetsMonument(monument);
        ShopManager.instance.IncrementPlayerMoney(-money);
    }
    public void BuyCharacter(int character, int money)
    {
        Debug.Log("Has comprado el personaje de indice de indice" + character + "me ha costado" + money);
        ShopManager.instance.IncrementPlayerAssetsCharacter(character);
        ShopManager.instance.IncrementPlayerMoney(-money);
    }
    #endregion
    #endregion
}
