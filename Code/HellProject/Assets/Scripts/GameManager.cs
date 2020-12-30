using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public List<Tourist> tourists = new List<Tourist>();
    private int currentLevel;
    private int playerCoins;
    private int playerGems;
    private float timeLeft;

    public static GameManager instance;
    //Ratings

    private void Awake()
    {
        instance = this;
        //if (instance){ Destroy(instance); instance = this; }
        DontDestroyOnLoad(gameObject);
        Save();

    }

    private void Start()
    {
        Load();
    }


    public void LoadGame(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void Save()
    {
        PlayerData playerData = new PlayerData
        {
            coins = playerCoins,
            gems = playerGems,
            timeLeft = timeLeft
        };
        string saveJSON = JsonUtility.ToJson(playerData);
        string filepath = Application.persistentDataPath + "/playerStats.json";
        File.WriteAllText(filepath,saveJSON);
    }

    public void DeleteSavedStats()
    {
        PlayerData playerData = new PlayerData
        {
            coins = 0,
            gems = 0,
            timeLeft = 0
        };
        string saveJSON = JsonUtility.ToJson(playerData);
        string filepath = Application.persistentDataPath + "/playerStats.json";
        File.WriteAllText(filepath, saveJSON);
    }

    public void Load()
    {
        string filepath = Application.persistentDataPath + "/playerStats.json";

        if(File.Exists(filepath))
        {
            string savedData = File.ReadAllText(filepath);
            Debug.Log(savedData);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(savedData);
            Debug.Log(playerData.ToString());

        }
        else
        {
            Debug.Log("There is no data saved");
        }
        
    }

    private class PlayerData
    {
        public int coins;
        public int gems;
        public float timeLeft;

        override
        public String ToString()
        {
            return "Coins:  " + coins + "\nGems: " + gems;
        }
    }
}
