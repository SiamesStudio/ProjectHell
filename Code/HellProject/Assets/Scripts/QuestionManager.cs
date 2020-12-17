using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuestionManager : MonoBehaviour
{
    private Queue<Question> questions;
    [SerializeField] private Text touristNameDisplay;
    [SerializeField] private Text questionDisplay;
    [SerializeField] private List<Text> answersDisplay;
    private Animator anim;
    private int correctAnswer;
    private Question currentQuestion;
    private bool isVisible;
    [SerializeField] private bool debugOptions;
    [HideInInspector] [SerializeField] private Tourist tourist;
    [HideInInspector] [SerializeField] private Slider timeSlider;

    public static QuestionManager instance;

    private void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;

        if (!TryGetComponent(out anim))
            Debug.LogError("QuestionManager error: No Animator Controller found in " + name);
        if (debugOptions) GenerateExQuestions();
    }

    private void Update()
    {
        if (questions == null) return;
        if (currentQuestion == null)
        {
            if(questions.Count > 0) PrintQuestion();
            else return;
        }

        if (LevelManager.instance.isQuestionable) MakeVisible();
        else MakeInvisible();

        currentQuestion.coolDown -= Time.deltaTime;
        timeSlider.value = currentQuestion.coolDown;
        if (currentQuestion.coolDown < 0) ReceiveAnswer(-1);

        if (debugOptions) DebugOptions();
    }

    /// <summary>
    /// Prints Question using UI Canvas. Called just once per question
    /// </summary>
    public void PrintQuestion()
    {
        currentQuestion = questions.Dequeue();

        touristNameDisplay.text = currentQuestion.tourist.name;
        questionDisplay.text = currentQuestion.question;
        timeSlider.maxValue = currentQuestion.coolDown;
        timeSlider.value = currentQuestion.coolDown;

        string _correctAnswer = currentQuestion.answers[0];
        ShuffleList(currentQuestion.answers);
        correctAnswer = currentQuestion.answers.IndexOf(_correctAnswer);

        for (int i = 0; i < currentQuestion.answers.Count; i++)
            answersDisplay[i].text = currentQuestion.answers[i];

    }

    /// <summary>
    /// Plays animation of showing question 
    /// </summary>
    public void MakeVisible()
    {
        if (isVisible) return;
        Debug.Log("Making it visible");
        isVisible = true;
        //anim.AnimName();
    }

    /// <summary>
    /// Plays animation of hiding question
    /// </summary>
    public void MakeInvisible()
    {
        if (!isVisible) return;
        Debug.Log("Making it invisible");
        //anim.AnimName
        isVisible = false;
    }

    /// <summary>
    /// Calls question's answer method. Called when answer selected or cooldown < 0
    /// </summary>
    /// <param name="_answer"></param>
    public void ReceiveAnswer(int _answer)
    {
        MakeInvisible();
        if(_answer == -1) //Not answered
        {
            currentQuestion.Answer(-1);
        }
        else if(_answer == correctAnswer)
        {
            currentQuestion.Answer(0);
            if(currentQuestion.isMonumentRelated)
            {
                if (LevelManager.instance.currentMonument.AddAnswer())
                    LevelManager.instance.UpdateMonument();
            }
        }
        else
        {
            currentQuestion.Answer(1);
        }
        currentQuestion = null;
    }

    /// <summary>
    /// Shuffles given list. Just a helper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_list"></param>
    private void ShuffleList<T>(List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            T _temp = _list[i];
            int randomIndex = Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = _temp;
        }
    }


    #region Debug
    private void DebugOptions()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    private void GenerateExQuestions()
    {
        questions = new Queue<Question>();
        string[] _answers = { "Good", "Bad" };
        Question _question = new Question("How r u?", new List<string>(_answers), tourist, true);
        questions.Enqueue(_question);

        string[] _answers2 = { "Yep", "Nope" };
        _question = new Question("U love me?", new List<string>(_answers2), tourist, true);
        questions.Enqueue(_question);
    }
    #endregion

    #region EDITOR CUSTOMIZATION

    #if UNITY_EDITOR
        [CustomEditor(typeof(QuestionManager), true)]
        public class QuestionManagerEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI(); //A work around for an Unity bug :/
                //DrawDefaultInspector(); // for other non-HideInInspector fields

                QuestionManager _script = (QuestionManager)target;

                if (_script.debugOptions) // if bool is true, show other fields
                {
                    _script.tourist = EditorGUILayout.ObjectField(_script.tourist, typeof(Tourist), true) as Tourist;
                    _script.timeSlider = EditorGUILayout.ObjectField(_script.timeSlider, typeof(Slider), true) as Slider;
                }
            }
        }
    #endif
    #endregion

}


