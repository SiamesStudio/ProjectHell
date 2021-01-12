using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopManager : MonoBehaviour
{
    [Header("Información general")]
    public Text coinsText;
    public Text gemsText;
    public Text timeText;
    public Text moneyText;

    [Header("Información del jugador")]
    public List<Tourist> tourists = new List<Tourist>();
    public int playerCoins;
    public int playerGems;
    //public float timeLeft;
    public int money;
    public float totalTime;
    public static ShopManager instance;
    void Start()
    {
        instance = this;
        //if (instance){ Destroy(instance); instance = this; }
        DontDestroyOnLoad(gameObject);
        playerCoins = GameManager.instance.playerCoins;
        money = GameManager.instance.playerMoney;
        playerGems = GameManager.instance.playerGems;
        totalTime = GameManager.instance.extraTime;
        UpdateText();    
    }

    public void IncrementPlayerCoins(int increment)
    {
        playerCoins += increment;
        UpdateText();
    }
    public void IncrementPlayerGems(int increment)
    {
        playerGems += increment;
        UpdateText();
    }
    public void IncrementTime(float time)
    {
        totalTime += time;
        UpdateText();
    }
    public void IncrementPlayerMoney(int increment)
    {
        money += increment;
        UpdateText();
    }
    public void IncrementPlayerAssetsCharacter(int increment)
    {
        //no se como hacerlo le paso el id 
    }
    public void IncrementPlayerAssetsMonument(int increment)
    {
        //igual que IncrementPlayerAssetsCharacter le paso el id
    }
    public void UpdateText()
    {

       // String textoTiempo = (totalTime / 60).ToString() + ":" + (totalTime % 60).ToString();
        coinsText.text = playerCoins.ToString();
        gemsText.text = playerGems.ToString();
        timeText.text = totalTime.ToString();
        moneyText.text = money. ToString();

    }
       
      
}
