using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool isQuestionVisible;
    public bool isQuestionable;
    [SerializeField] private List<Monument> monuments;
    [HideInInspector] public Monument currentMonument;

    public static LevelManager instance;

    private void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;
        currentMonument = monuments[0];
    }

    public void UpdateMonument()
    {
        Debug.Log("Level Manager: Monument Updated!");
    }
}
