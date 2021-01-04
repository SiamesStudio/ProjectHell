using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
using System.Linq;

public class Tourist : MonoBehaviour
{
    [SerializeField] private Animator anim;
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
    public float happiness = 100;
    private bool leaving;
    #endregion

    [SerializeField] private SkinnedMeshRenderer hairMeshRenderer;
    [SerializeField] private SkinnedMeshRenderer[] applyMaterialTo;


    #endregion


    #region methods

    void Start()
    {
        GameManager.instance.tourists.Add(this);
        character = TouristManager.instance.GenerateCharacter();
        name = character.name;
        character.GenerateDictionary();
        GenerateQuestions();
        PrintCharacter();
    }

    void Update()
    {
        anim.SetBool("isWalking", !GetComponent<RTSAgent>().isPositioned);
        //DebugInput();
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

    public float GenerateRating()
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

        float _rating = Mathf.Round(10 * rightAnswers / emptyAnswers); //0 - 10
        return _rating;
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
        GetComponent<RTSAgent>().enabled = false;
        GetComponent<NavMeshAgent>().SetDestination(LevelManager.instance.startPoint.position);
        leaving = true;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (!leaving) return;
        if(other.CompareTag("Entrance"))
        {
            Die();
        }
    }

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
    #endregion

    #region Debug
    private void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.Space)) AskQuestion();
        if (Input.GetKeyDown(KeyCode.Return)) GenerateRating();
        if (Input.GetKeyDown(KeyCode.L)) Leave();
        if (Input.GetKeyDown(KeyCode.P)) PrintCharacter();
    }

    #endregion

    public void PrintCharacter()
    {
        hairMeshRenderer.sharedMesh = TouristManager.instance.skinParts[character.hair];
        foreach(SkinnedMeshRenderer _renderer in applyMaterialTo)
        {
            _renderer.material = character.material;
        }
        //material.SetTexture("_MainTex", character.materialTex);
        //material.mainTexture = character.materialTex;
        //material.color = new Color(UnityEngine.Random.Range(0, 255), UnityEngine.Random.Range(0, 255), UnityEngine.Random.Range(0, 255));
    }
}
