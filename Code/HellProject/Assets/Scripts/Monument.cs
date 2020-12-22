using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : MonoBehaviour
{
    public enum MonumentType { PutoJudas, PacoPorros};

    public MonumentType id;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RTSZone")) LevelManager.instance.isQuestionable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RTSZone")) LevelManager.instance.isQuestionable = false;
    }
}
