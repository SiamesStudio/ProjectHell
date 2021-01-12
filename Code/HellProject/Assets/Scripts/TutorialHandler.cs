using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    private int index = 0;
    [SerializeField] string[] texts;
    [SerializeField] Text textDisplay;
    [SerializeField] Animator anim;
    private Demon demon;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(index < texts.Length) textDisplay.text = texts[index];
        switch (index)
        {
            case 0:
                if(Input.GetMouseButtonDown(0)) //Moverse
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit _hit))
                        ShowNewMessage();
                }
                break;
            case 1: //Llegar a la zona de monumento -> Mensajito de dar a que se paren
                if (LevelManager.instance.isQuestionable) ShowNewMessage();
                break;
            case 2: //Hacer que paren los turistas ahí -> Mensajito + Spawnear pregunta
                if (LevelManager.instance.isQuestionable && !LevelManager.instance.touristsZone.following)
                {
                    ShowNewMessage();
                    GameManager.instance.tourists[0].character.waitTime = 100f;
                    GameManager.instance.tourists[0].AskQuestion();
                }
                break;
            case 3:
                //NOTHING HEHEHE
                break;
            case 4: //Contestar pregunta -> Mensajito y spawnear demonio
                // hehehe already handled -> callback
                break;
            case 5:
                if (Vector3.Distance(LevelManager.instance.touristsZone.transform.position, demon.transform.position) < 30f)
                {
                    ShowNewMessage();
                    Time.timeScale = 0;
                }
                break;
            case 6:
                if (Input.GetMouseButtonDown(0)) //Moverse
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit _hit))
                    {
                        if (_hit.transform.gameObject.TryGetComponent(out Demon _demon))
                        {
                            Time.timeScale = 1;
                            ShowNewMessage();
                        }
                    }
                }
                break;
            case 7:
                if (demon == null)
                {
                    LevelManager.instance.endArea.SetActive(true);
                    ShowNewMessage();
                }
                break;
        }
    }

    public void OnAnswerClicked()
    {

        if (index != 4) return;
        demon = LevelManager.instance.currentMonument.Spawn();
        ShowNewMessage();
    }

    public void OnQuestionOpened()
    {
        if (index == 3) ShowNewMessage();
    }

    private void ShowNewMessage()
    {
        anim.SetTrigger("NewMessage");
        index++;
    }
}
