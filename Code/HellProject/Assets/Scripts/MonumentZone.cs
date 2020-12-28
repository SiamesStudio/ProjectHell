using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RTSZone"))
        {
            LevelManager.instance.isQuestionable = true;
            DemonManager.instance.SetSpawning(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RTSZone"))
        {
            LevelManager.instance.isQuestionable = false;
            DemonManager.instance.SetSpawning(true);
        }
    }
}
