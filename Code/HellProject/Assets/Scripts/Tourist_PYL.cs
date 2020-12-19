using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Tourist : MonoBehaviour
{
    // Start is called before the first frame update
    #region variables

    #region variablesUpdatedManager
    private string name; // lo genera el manager
    string path = "Assets/TXT/test.txt";
    private TextAsset genericQuestions;// lo genera el manager
    private TextAsset monumentQuestions;// lo genera el manager
    private TextAsset emptyRating;// lo genera el manager
                                  //private ArrayList<Question> currentQuestions;
    #endregion

    #region variablesUpdatedGame
    public bool targeted;
    public bool kidnapped;
    private bool dying;
    Vector2 questionsCooldown;//no recuerdo para que era
    private int rightAnswers;
    private int wrongAnswers;
    private int emptyAnswers;
    private float happiness;
    private float waitTime;
    #endregion
    #endregion


    #region methods
    void Start()
    {
        targeted = false;
        dying = false;
        kidnapped = false;
        happiness = 1.0f; // habrá que castear diciendo si va de 0-100 o de 0-1;
        rightAnswers = 0;
        wrongAnswers = 0;
        emptyAnswers = 0;
        ReadString(path, name);

    }
    void Update()
    {
        die();
    }
    public void GenerateQuestions()
    {

    }

    public void AskQuestion()
    {

    }
    public void GenerateRating()
    {
       
    }
    static void ReadString(String path, String name)
    {
        

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        name = reader.ReadLine();
        Debug.Log(name);
        reader.Close();
    }
    public void Leave()
    { // cuanto de felicidad se va el personaje

    }
    public void die()
    {
        if (dying)
        {
            Destroy(this.gameObject);

        }
    }
    #endregion
    #region getters and setters
    public bool GetTargeted()
    {
        return targeted;
    }
    public void SetTargeted(bool newTargeted)
    {
        targeted = newTargeted;
    }
    public bool GetKidnapped()
    {
        return kidnapped;
    }
    public void SetKidnapped(bool newKidnapped)
    {
        kidnapped = newKidnapped;
    }
    public bool GetDying()
    {
        return dying;
    }
    public void SetDying(bool newDying)
    {
        dying = newDying;
    }
    #endregion
}
