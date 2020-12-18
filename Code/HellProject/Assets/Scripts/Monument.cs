using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : MonoBehaviour
{
    enum monument { PutoJudas, PacoPorros}; //This should probably be in the GameManager

    [SerializeField] private monument id;
    private int answersDone;
    [SerializeField] private int totalAnswers;

    /// <summary>
    /// Adds a new answer. Returns true if completed
    /// </summary>
    /// <returns></returns>
    public bool AddAnswer()
    {
        Debug.Log("Monument: Adding Answer");
        //PARTICLES AND SHIT!
        return ++answersDone >= totalAnswers;
    }
}
