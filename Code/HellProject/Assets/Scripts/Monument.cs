using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : MonoBehaviour
{
    public enum MonumentType { PutoJudas, PacoPorros};

    public MonumentType id;
    private int answersDone;
    [SerializeField] private int totalAnswers;
    [Header("Zones")]
    [SerializeField] private MonumentZone[] monumentZones;
    [SerializeField] private int[] monumentsUnlockAt;
    private int currentZoneId = 0;


    /// <summary>
    /// Adds a new answer. Returns true if completed
    /// </summary>
    /// <returns></returns>
    public bool AddAnswer()
    {
        Debug.Log("Monument: Adding Answer");
        answersDone++;
        if (monumentsUnlockAt.Length > 0)
        {
            try
            {
                if (monumentsUnlockAt[currentZoneId + 1] > answersDone)
                {
                    monumentZones[currentZoneId].gameObject.SetActive(false);
                    currentZoneId++;
                    monumentZones[currentZoneId].gameObject.SetActive(true);
                }
            }
            catch (System.Exception e) { }
        }
        //PARTICLES AND SHIT!
        return answersDone >= totalAnswers;
    }
}
