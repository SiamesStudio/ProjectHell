using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public string question;
    public List<string> answers;
    public Tourist tourist;
    public bool isMonumentRelated;
    public float coolDown;

    public Question(string _question, List<string> _answers, Tourist _tourist, bool _isMonumentRelated)
    {
        question = _question;
        answers = _answers;
        tourist = _tourist;
        isMonumentRelated = _isMonumentRelated;
        coolDown = _tourist.waitTime;
    }

    public void Answer(int _answerID)
    {
        switch(_answerID)
        {
            case -1:
                tourist.emptyAnswers++;
                Debug.Log("Not Answered!");
                break;
            case 0:
                tourist.rightAnswers++;
                Debug.Log("Correct!");
                break;
            default:
                tourist.wrongAnswers++;
                Debug.Log("Incorrect!");
                break;
        }
    }
}
