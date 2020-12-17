using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : MonoBehaviour
{
    private int answersDone;
    [SerializeField] private int totalAnswers;

    /// <summary>
    /// Adds a new answer. Returns true if completed
    /// </summary>
    /// <returns></returns>
    public bool AddAnswer()
    {
        Debug.Log("Monument: Adding Answer");
        return ++answersDone >= totalAnswers;
    }
}
