using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && GameManager.instance.touristsAvailable.Count > 0)
        {
            GameManager.instance.playerCoins += (5 * GameManager.instance.tourists.Count);
            GameManager.instance.playerGems += (GameManager.instance.tourists.Count);
            GameManager.instance.FadeToLevel(0);
        }
    }
}
