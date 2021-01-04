using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuestionManager : MonoBehaviour
{
    public Queue<Question> questions = new Queue<Question>();
    [SerializeField] private Text touristNameDisplay;
    [SerializeField] private Text questionDisplay;
    [SerializeField] private Button showButton;
    [SerializeField] private List<Text> answersDisplay;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private Text happinessDisplay;
    [SerializeField] private Vector2 timeBetweenQuestions;
    private float questionCountDown = 0;
    private Animator anim;
    private int correctAnswer;
    private Question currentQuestion;
    private bool isVisible;

    [SerializeField] string[] happinessIndicators;

    [SerializeField] private bool debugOptions;
    [HideInInspector] [SerializeField] private Tourist tourist;
    [HideInInspector] [SerializeField] private bool freezeCoolDown;

    public static QuestionManager instance;

    private void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;
        showButton.gameObject.SetActive(false);
        touristNameDisplay.gameObject.SetActive(false);

        if (!TryGetComponent(out anim))
            Debug.LogError("QuestionManager error: No Animator Controller found in " + name);
    }

    private void Update()
    {
        questionCountDown -= Time.deltaTime;
        if (debugOptions) DebugOptions();

        if (LevelManager.instance.isQuestionVisible && currentQuestion != null) showButton.gameObject.SetActive(true); //MakeVisible();
        else
        {
            showButton.gameObject.SetActive(false);
            touristNameDisplay.gameObject.SetActive(false);
            MakeInvisible();
        }
        if (currentQuestion == null)
        {
            if(questions.Count > 0 && LevelManager.instance.isQuestionable && questionCountDown < 0) 
                PrintQuestion();
            else return;
        }


            
        currentQuestion.coolDown -= Time.deltaTime;
        if(debugOptions && !freezeCoolDown) currentQuestion.coolDown -= Time.deltaTime;
        timeSlider.value = currentQuestion.coolDown;
        if (currentQuestion.coolDown < 0) ReceiveAnswer(-1);

    }

    /// <summary>
    /// Prints Question using UI Canvas. Called just once per question
    /// </summary>
    public void PrintQuestion()
    {
        currentQuestion = questions.Dequeue();

        touristNameDisplay.text = currentQuestion.tourist.character.name;
        questionDisplay.text = currentQuestion.question;
        timeSlider.maxValue = currentQuestion.coolDown;
        timeSlider.value = currentQuestion.coolDown;

        string _correctAnswer = currentQuestion.answers[0];
        ShuffleList(currentQuestion.answers);
        correctAnswer = currentQuestion.answers.IndexOf(_correctAnswer);

        for (int i = 0; i < currentQuestion.answers.Count; i++)
            answersDisplay[i].text = currentQuestion.answers[i];

        int _discreteHappiness = Mathf.FloorToInt(currentQuestion.tourist.happiness / (100 / happinessIndicators.Length));
        if (_discreteHappiness > happinessIndicators.Length) _discreteHappiness =  happinessIndicators.Length;
        Debug.Log(_discreteHappiness);
        happinessDisplay.text = happinessIndicators[_discreteHappiness];

    }

    public void ShowPerformed()
    {
        isVisible = !isVisible;
        touristNameDisplay.gameObject.SetActive(isVisible);
        anim.SetBool("isVisible", isVisible);
    }

    /// <summary>
    /// Plays animation of showing question 
    /// </summary>
    public void MakeVisible()
    {
        if (isVisible) return;
        isVisible = true;
        anim.SetBool("isVisible", isVisible);
    }

    /// <summary>
    /// Plays animation of hiding question
    /// </summary>
    public void MakeInvisible()
    {
        if (!isVisible) return;
        isVisible = false;
        anim.SetBool("isVisible", isVisible);
    }

    /// <summary>
    /// Calls question's answer method. Called when answer selected or cooldown < 0
    /// </summary>
    /// <param name="_answer"></param>
    public void ReceiveAnswer(int _answer)
    {
        if (currentQuestion == null) return;
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
        MakeInvisible();
        currentQuestion = null;
        questionCountDown = Random.Range(timeBetweenQuestions.x, timeBetweenQuestions.y);
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LevelManager.instance.isQuestionable = !LevelManager.instance.isQuestionable;
            Debug.Log("Key pressed: Q");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            LevelManager.instance.isQuestionVisible = !LevelManager.instance.isQuestionVisible;
            Debug.Log("Key pressed: V");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateExQuestion();
            Debug.Log("Key pressed: G");
        }
    }

    private void GenerateExQuestion()
    {        
        string[] _answers = { "Good", "Bad" };
        Question _question = new Question("How r u?", new List<string>(_answers), tourist, true);
        questions.Enqueue(_question);
        Debug.Log("Example question generated. Count: " + questions.Count);
        /*
        string[] _answers2 = { "Yep", "Nope" };
        _question = new Question("U love me?", new List<string>(_answers2), tourist, true);
        questions.Enqueue(_question); */
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
                    _script.freezeCoolDown = EditorGUILayout.Toggle("Freeze CoolDown", _script.freezeCoolDown);
                    _script.tourist = EditorGUILayout.ObjectField(_script.tourist, typeof(Tourist), true) as Tourist;
                }
            }
        }
    #endif
    #endregion

}


