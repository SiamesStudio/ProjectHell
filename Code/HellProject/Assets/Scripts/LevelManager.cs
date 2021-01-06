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
    public Transform startPoint;
    [SerializeField] private List<Monument> monuments;
    [HideInInspector] public Monument currentMonument;

    public static LevelManager instance;

    [Header("Audio")]
    [SerializeField] AudioClip lavaSound;
    [SerializeField] AudioSource lavaSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip[] musicList;
    private int currentSongId=-1;
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

        musicSource.volume = 0.6f;
        InvokeRepeating("CheckIfSongEnded", 0.25f, 2);
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

        //foreach Tourist  _tourist in in tourists _tourist.GenerateQuestions(); -> ahora mismo la lista de tourists es la que es
    }

    #endregion
}
