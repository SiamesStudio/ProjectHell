using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuLogic : MonoBehaviour
{
    [SerializeField] Button chaptersButton;
    [SerializeField] Button bundlesButton;
    [SerializeField] Button coinsButton;
    [SerializeField] Button playButton;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject upgradesUI;
    [SerializeField] GameObject levelsUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        GameManager.instance.LoadGame("Joselu");
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

    public void OnLevelsButton()
    {
        shopUI.SetActive(false);
        levelsUI.SetActive(true);
    }

}
