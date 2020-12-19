using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Tourist : MonoBehaviour
{
    [HideInInspector] public float waitTime;

    [HideInInspector] public int rightAnswers = 0;
    [HideInInspector] public int wrongAnswers = 0;
    [HideInInspector] public int emptyAnswers = 0;
    // Start is called before the first frame update
    #region variables

    #region variablesUpdatedManager
    private string name;
    string path = "Assets/TXT/test.txt";
    private TextAsset genericQuestions;
    private TextAsset monumentQuestions;
    private TextAsset emptyRating;
    private List<Question> currentQuestions;
    #endregion

    #region variablesUpdatedGame
    public bool targeted;
    public bool kidnapped;
    private bool dying;
    private float questionsCooldown;
    private float happiness;
    #endregion
    #endregion

    #region methods
    void Start()
    {
        targeted = false;
        dying = false;
        kidnapped = false;
        happiness = 1.0f;
        rightAnswers = 0;
        wrongAnswers = 0;
        emptyAnswers = 0;
     //   ReadString(path, name);

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
  /*  static void ReadString(String path, String name)
    {
        StreamReader reader = new StreamReader(path);
        name = reader.ReadLine();
        Debug.Log(name);
        reader.Close();
    }*/
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
