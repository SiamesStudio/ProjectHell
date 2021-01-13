using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class Tourist : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [HideInInspector] public int rightAnswers = 0;
    [HideInInspector] public int wrongAnswers = 0;
    [HideInInspector] public int emptyAnswers = 0;
    public bool isQuestionable = true;

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
    public bool isLeaving;
    private float questionTimeOut;
    #endregion

    [SerializeField] private SkinnedMeshRenderer hairMeshRenderer;
    [SerializeField] private SkinnedMeshRenderer[] applyMaterialTo;

    [Header("Audio")]
    [SerializeField] AudioClip freeSound;
    [SerializeField] AudioClip agreeSound;
    [SerializeField] AudioClip disagreeSound;
    [SerializeField] AudioClip notReceivingAnswerSound;
    private AudioSource audioSource;

    #endregion

    public Canvas myCanvas;
    public Image fillImage;


    #region methods

    void Start()
    {
        GameManager.instance.name = "Joselu";
        Debug.Log(GameManager.instance);
        GameManager.instance.tourists.Add(this);
        character = TouristManager.instance.GenerateCharacter();
        name = character.name;
        character.GenerateDictionary();
        GenerateQuestions();
        PrintCharacter();
        questionTimeOut = UnityEngine.Random.Range(character.questionCoolDown.x, character.questionCoolDown.y);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        isQuestionable = !isLeaving && !kidnapped;
        anim.SetBool("isWalking", !GetComponent<RTSAgent>().isPositioned || isLeaving);
        //DebugInput();
        questionTimeOut -= Time.deltaTime;
        if (questionTimeOut <= 0 && !LevelManager.instance.isTutorial)
        {
            questionTimeOut = UnityEngine.Random.Range(character.questionCoolDown.x, character.questionCoolDown.y);
            AskQuestion();
        }
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
        if (!TryGetComponent(out RTSAgent _rtsAgent)) return;
        GetComponent<RTSAgent>().enabled = false;
        GetComponent<NavMeshAgent>().SetDestination(LevelManager.instance.startPoint.position);
        isLeaving = true;
        isQuestionable = false;
        myCanvas.gameObject.SetActive(false);
    }

    public void Die()
    {
        GameManager.instance.tourists.Remove(this);
        Destroy(gameObject);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (!isLeaving) return;
        if(other.CompareTag("Entrance"))
        {
            Die();
        }
    }

    public void PlayDisagreementSound()
    {
        audioSource.clip = disagreeSound;
        audioSource.Play();
    }

    public void PlayAgreementSound()
    {
        if (!audioSource) return;
        audioSource.clip = agreeSound;
        audioSource.Play();
    }

    public void PlayIgnoredSound()
    {
        audioSource.clip = notReceivingAnswerSound;
        audioSource.Play();
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
    public void SetKidnapped(bool isKidnapped)
    {
        if (isKidnapped) anim.SetTrigger("Kidnapped");
        anim.SetBool("isWalking", !isKidnapped);
        isQuestionable = !isKidnapped;
        kidnapped = isKidnapped;
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
