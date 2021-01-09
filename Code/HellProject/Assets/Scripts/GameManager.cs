using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public List<Tourist> tourists = new List<Tourist>();
    public List<Tourist> touristsAvailable = new List<Tourist>();
    private int currentLevel;
    [HideInInspector] public int playerCoins;
    [HideInInspector] public int playerGems;
    [HideInInspector] public float playerTimeLeft;
    [HideInInspector] public int playerNumTourists;

    public static GameManager instance;
    [SerializeField] bool isDebugging;
    //Ratings

    private void Awake()
    {
        instance = this;
        //if (instance){ Destroy(instance); instance = this; }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Load();
    }

    public void Update()
    {
        if(isDebugging)
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                OnPlayerDeath();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                playerCoins+=10;
            }
        }
        UpdateTourists();

    }

    private void UpdateTourists()
    {
        touristsAvailable.Clear();
        foreach(Tourist _tourist in tourists)
        {
            if (_tourist.isQuestionable) touristsAvailable.Add(_tourist);
        }
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
            timeLeft = playerTimeLeft,
            numTourists = touristsAvailable.Count
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
            timeLeft = 0,
            numTourists = 0
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
            playerCoins = playerData.coins;
            playerGems = playerData.gems;
            playerTimeLeft = playerData.timeLeft;
            playerNumTourists = playerData.numTourists;
        }
        else
        {
            Debug.Log("There is no data saved");
        }
    }

    public void OnPlayerDeath()
    {
        Save();
        LoadGame("Menu");
    }

    private class PlayerData
    {
        public int coins;
        public int gems;
        public float timeLeft;
        public int numTourists;

        override
        public String ToString()
        {
            return "Coins:  " + coins + "\nGems: " + gems + "\ntimeLeft: " + timeLeft + "\nnumTourists: " + numTourists;
        }
    }
}
