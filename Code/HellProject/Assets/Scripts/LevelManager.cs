using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region variables
    [Header("Audio")]
    [SerializeField] AudioClip lavaSound;
    [SerializeField] AudioSource lavaSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip[] musicList;
    private int currentSongId = -1;

    [Header("End Game")]
    public Text timeTextleft;
    private float timeLevel;
    public GameObject endArea;

    [Header("Others")]
    public bool isQuestionVisible;
    public bool isQuestionable;
    public Transform startPoint;
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

    private void Start()
    {
        lavaSource.clip = lavaSound;
        lavaSource.loop = true;
        lavaSource.Play();
        lavaSource.volume = 0.5f;
        timeLevel = GameManager.instance.playerTimeLeft;

        musicSource.volume = 0.6f;
        InvokeRepeating("CheckIfSongEnded", 0.25f, 2);
    }
    void Update()
    {
        timeTextleft.text = (timeLevel-(int)Time.time).ToString();
        if ((timeLevel - (int)Time.time) <= 0 || GameManager.instance.tourists.Count<=0) Loser();


    }
    private void CheckIfSongEnded()
    {
        if (!musicSource.isPlaying)
        {
            PlayNewSong();
        }
    }

    private void PlayNewSong()
    {
        AudioClip _clip = GetSongFromList(musicList);
        musicSource.clip = _clip;
        musicSource.Play();
    }

    private AudioClip GetSongFromList(AudioClip[] musicList)
    {
        int songId;
        do
        {
            songId = Random.Range(0, musicList.Length);
        } while (songId == currentSongId);
        currentSongId = songId;

        return musicList[songId];
    }

    public void UpdateMonument()
    {
        Debug.Log("Level Manager: Monument Updated!");
        //AQUI SE GENERA EL PODER AVANZAR
        currentMonument = monuments[monuments.IndexOf(currentMonument) + 1];
        if (monuments.IndexOf(currentMonument) >= monuments.Count) endArea.GetComponent<Collider>().enabled = true;
        //foreach Tourist  _tourist in in tourists _tourist.GenerateQuestions(); -> ahora mismo la lista de tourists es la que es
    }
    public void Loser()
    {
        Debug.Log("He perdido");
        // panel de volver a jugar
        //Cambiar de escena automaticamente( no se si vamos a querer hacer un reset de las cosas que haya conseguido en el nivel.

    }

    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.gameObject.TryGetComponent(out Player player))
        {

            GameManager.instance.playerCoins+= (5 * GameManager.instance.tourists.Count);
            GameManager.instance.playerGems += (1* GameManager.instance.tourists.Count);
            Debug.Log("He ganado el nivel");
            //Cambiar de escena automaticamente( no se si vamos a querer hacer un reset de las cosas que haya conseguido en el nivel.
        }

    }

    #endregion
}
