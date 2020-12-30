﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentZone : MonoBehaviour
{
    [SerializeField] private Monument myMonument;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RTSZone"))
        {
            LevelManager.instance.isQuestionable = true;
            myMonument.SetSpawning(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RTSZone"))
        {
            LevelManager.instance.isQuestionable = false;
            myMonument.SetSpawning(false);
        }
    }
}
