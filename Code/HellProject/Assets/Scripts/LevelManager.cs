using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class LevelManager : MonoBehaviour
{
    #region variables
    public bool isQuestionVisible;
    public bool isQuestionable;
    [SerializeField] private List<Monument> monuments;
    [HideInInspector] public Monument currentMonument;

    public static LevelManager instance;

    #endregion


    #region methods

    private void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;
        currentMonument = monuments[0];

    }
    public void UpdateMonument()
    {
        Debug.Log("Level Manager: Monument Updated!");
        //AQUI SE GENERA EL PODER AVANZAR
        currentMonument = monuments[monuments.IndexOf(currentMonument) + 1];
    }

    #endregion
}
