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
    [Header("Componentes")]
    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject creditsUI;

    [Header("Información del jugador")]
    public Text coinsText;
    public Text gemsText;
    public Text timeText;
    private int playerCoins;
    private int playerGems;
    private float totalTime;

    /* [SerializeField] Button chaptersButton;
     [SerializeField] Button bundlesButton;
     [SerializeField] Button coinsButton;
     [SerializeField] Button playButton;
     [SerializeField] Button startButton;
     [SerializeField] GameObject upgradesUI;*/
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject levelsUI;
    #region Alguien 
    

    #region Store
    public void OnChaptersButton()
    {

    }

    public void OnBundlesButton()
    {

    }

    public void OnCoinsButton()
    {

    }

    #endregion

    public void OnPlayButton()
    {
        GameManager.instance.FadeToLevel(1);
    }

    public void OnMenuButton()
    {

    }

    public void OnNextButton()
    {

    }
     public void OnStoreButton()
    {
        shopUI.SetActive(true);
        levelsUI.SetActive(false);
    }
   
    public void OnStartButton()
    {
        
        menuUI.GetComponent<Canvas>().enabled = false;
    }

    public void OnLevelsButton()
    {
        shopUI.SetActive(false);
        levelsUI.SetActive(true);
    }
    #endregion
    #region Patrii
    void Start()
    {
        playerCoins = GameManager.instance.playerCoins;
        playerGems = GameManager.instance.playerGems;
        totalTime = GameManager.instance.playerTimeLeft;
        PlayerDataOut();
    }
    public void OnEnterGameButton()
    {
        menuUI.GetComponentInChildren<Canvas>().enabled = false;
        gameUI.GetComponentInChildren<Canvas>().enabled = true;

    }
    public void OnCreditsGameButton()
    {
        menuUI.GetComponentInChildren<Canvas>().enabled = false;
        creditsUI.GetComponentInChildren<Canvas>().enabled = true;

    }
    public void OnLevelGameButton( int level)
    {
        switch (level)
        {
            case 0:
                //cambio a pantalla de juego nivel 1
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
                menuUI.GetComponentInChildren<Canvas>().enabled = true;
                gameUI.GetComponentInChildren<Canvas>().enabled = false;
                break;
            case 1:
                menuUI.GetComponentInChildren<Canvas>().enabled = true;
                creditsUI.GetComponentInChildren<Canvas>().enabled = false;
                break;
        }

    }
    public void PlayerDataOut()
    {

        coinsText.text = playerCoins.ToString();
        gemsText.text = playerGems.ToString();
        timeText.text = totalTime.ToString();

    }
    #endregion

}
