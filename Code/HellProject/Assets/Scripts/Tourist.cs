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
    public float happiness = 100;
    #endregion


    #endregion


    #region methods

    void Start()
    {
        GameManager.instance.tourists.Add(this);
        character = TouristManager.instance.GenerateCharacter();
        name = character.name;
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
        if (currentQuestionId >= currentQuestions.Count) currentQuestionId = 0;
        QuestionManager.instance.questions.Enqueue(currentQuestions[currentQuestionId]);
        currentQuestionId++;
    }

    public void GenerateRating()
    {
        Debug.Log("Right: " + rightAnswers);
        Debug.Log("Wrong: " + wrongAnswers);
        Debug.Log("Empty: " + emptyAnswers);

        int _totalAnswers = rightAnswers + wrongAnswers + wrongAnswers + emptyAnswers;
        int _random = UnityEngine.Random.Range(0, _totalAnswers);
        if (_random < rightAnswers) //RIGHT ANSWERS FILE
        {

        }
        else if (_random < rightAnswers + wrongAnswers) //WRONG ANSWERS FILE
        {

        }
        else if (_random < _totalAnswers) //EMPTY ANSWERS FILE
        {

        }
    }

    public void AddHappiness(int _happQuantity)
    {
        happiness += _happQuantity;
        if(_happQuantity < 0)
        {
            _happQuantity = 0;
            Leave();
        }
        if (_happQuantity > 100)
            _happQuantity = 100;
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
