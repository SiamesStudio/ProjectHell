using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public new string name;
    public float waitTime;
    [SerializeField] [Range(0, 100)] float monRelatedPerc;
    [SerializeField] private TextAsset genericQuestions;
    [SerializeField] private TextAsset[] monumentQuestionsList;

    public TouristManager.SkinPart hair;
    public Material material;

    //[SerializeField] private TextAsset emptyRating;
    //[SerializeField] private TextAsset rightRating;
    //[SerializeField] private TextAsset wrongRating;

    public Dictionary<Monument.MonumentType, TextAsset> monumentQuestions;

    public void GenerateDictionary()
    {
        List<Monument.MonumentType> _monuments = new List<Monument.MonumentType>();

        foreach (Monument.MonumentType _monumentType in System.Enum.GetValues(typeof(Monument.MonumentType)))
            _monuments.Add(_monumentType);

        monumentQuestions = new Dictionary<Monument.MonumentType, TextAsset>();
        for (int i = 0; i < monumentQuestionsList.Length; i++)
        {
            monumentQuestions.Add(_monuments[i], monumentQuestionsList[i]);
        }
    }

    public List<Question> QuestionsToList(Tourist _tourist)
    {
        List<Question> _questions = new List<Question>();

        List<string> _genericQuestions = new List<string> 
            (genericQuestions.text.Split(System.Environment.NewLine.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries));

        TextAsset _monumentsText = monumentQuestions[LevelManager.instance.currentMonument.id];
        List<string> _monumentQuestions = new List<string>
            (_monumentsText.text.Split(System.Environment.NewLine.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries));


        for(int i = 0; i < _monumentQuestions.Count; i+=3)
        {
            string[] _answers = { _monumentQuestions[i+1], _monumentQuestions[i+2] };
            Question _question = new Question(_monumentQuestions[i], new List<string>(_answers), _tourist, true);
            _questions.Add(_question);
        }
        int _numGenQuestions = (int) ((_questions.Count * (100 - monRelatedPerc)) / monRelatedPerc);

        List<Question> _genericQuestionsList = new List<Question>();
        for (int i = 0; i < _genericQuestions.Count; i += 3)
        {
            string[] _answers = { _genericQuestions[i + 1], _genericQuestions[i + 2] };
            _genericQuestionsList.Add(new Question(_genericQuestions[i], new List<string>(_answers), _tourist, false));
        }

        for(int i = 0; i < _numGenQuestions && i < _genericQuestionsList.Count; i++)
        {
            _questions.Add(_genericQuestionsList[i]);
        }

        ShuffleList(_questions);

        return _questions;
    }


    //METODO PARA HACER SHUFFLE DE LA LISTA
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
}
