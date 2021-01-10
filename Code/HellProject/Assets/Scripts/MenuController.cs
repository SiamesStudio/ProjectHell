using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Botones interfaz")]
    public Button enterGameButton;
    public Button creditButton;
    public Button backButton;
    [Header("Componentes")]
    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject creditsUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
