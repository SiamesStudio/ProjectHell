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
   /* [HideInInspector] public int playerCoins;
    [HideInInspector] public int playerGems;
    [HideInInspector] public float playerTimeLeft;
    [HideInInspector] public int playerNumTourists;*/

    public static GameManager instance;
    [SerializeField] bool isDebugging;


    [Header("Player's variable")]
    public int playerCoins;
    public int playerGems;
    public float extraTime;
    public int playerNumTourists;
    public int playerMoney;

    public Animator animator;
    [SerializeField] GameObject dirtyBackground;
    public int iterations;
    //Ratings

    private void Awake()
    {
        if (instance){ Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Load();
    }

    public void Update()
    {
        LevelManager.instance.enabled = true;
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
        if (tourists == null) return;
        foreach(Tourist _tourist in tourists)
        {
            if (_tourist.isQuestionable) touristsAvailable.Add(_tourist);
        }
    }
    public void FadeToLevel(int _lvlIndx)
    {
        iterations++;
        //levelToLoad = level;
        //animator.gameObject.SetActive(true);
        animator.SetTrigger("Fade");
        StartCoroutine(LoadLevelIn(_lvlIndx, 1f));
    }

    private IEnumerator LoadLevelIn(int _lvlIdx, float _time)
    {
        while(_time > 0)
        {
            _time -= Time.deltaTime;
            yield return null;
        }
        if (_lvlIdx != 0) dirtyBackground.SetActive(true);
        SceneManager.LoadScene(_lvlIdx, 0);
    }
    
    public void OnFadeOutComplete()
    {
       // SceneManager.LoadScene(levelToLoad);
       // animator.SetTrigger("FadeOut");
    }
    public void ResetScene()
    {
        //Scene currentScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void Save()
    {
        PlayerData playerData = new PlayerData
        {
            coins = playerCoins,
            gems = playerGems,
            timeLeft = extraTime,
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
            extraTime = playerData.timeLeft;
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
        FadeToLevel(0);
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
