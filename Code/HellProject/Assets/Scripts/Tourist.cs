using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Tourist : MonoBehaviour
{
    [HideInInspector] public int rightAnswers = 0;
    [HideInInspector] public int wrongAnswers = 0;
    [HideInInspector] public int emptyAnswers = 0;

    #region variables

    #region variablesUpdatedManager

    public Character character;
    public List<Question> currentQuestions;
    private int currentQuestionId = 0;
    #endregion

    #region variablesUpdatedGame
    [HideInInspector] public bool targeted;
    [HideInInspector]public bool kidnapped;
    private bool dying;
    private float questionsCooldown;
    private float happiness;
    #endregion


    #endregion


    #region methods
    void Start()
    {
        character = TouristManager.instance.GenerateCharacter();
        character.GenerateDictionary();
        GenerateQuestions();
    }

    void Update()
    {
        Die();

        DebugInput();
    }

    public void GenerateQuestions()
    {
        currentQuestions = character.QuestionsToList(this);
    }

    public void AskQuestion()
    {
        Debug.Log("Question Asked");
        if (currentQuestionId >= currentQuestions.Count) currentQuestionId = 0;
        QuestionManager.instance.questions.Enqueue(currentQuestions[currentQuestionId]);
        currentQuestionId++;
    }

    public void GenerateRating()
    {
        Debug.Log("Right: " + rightAnswers);
        Debug.Log("Wrong: " + wrongAnswers);
        Debug.Log("Empty: " + emptyAnswers);
    }

    public void Leave()
    {

    }

    public void Die()
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

    #region Debug
    private void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.Space)) AskQuestion();
        if (Input.GetKeyDown(KeyCode.Return)) GenerateRating();
    }

    #endregion
}
