using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : MonoBehaviour
{
    public enum MonumentType { PutoJudas, PacoPorros};

    public MonumentType id;
    private int answersDone;
    [SerializeField] private int totalAnswers;
    [Header("Zones")]
    [SerializeField] private MonumentZone[] monumentZones;
    [SerializeField] private int[] monumentsUnlockAt;
    private int currentZoneId = 0;

    [Header("Spawner")]
    public float spawnTime;
    [SerializeField] private List<Transform> spawnPoint;
    [SerializeField] private int maxDemons;
    [Header("Kind of Demons")]
    [SerializeField] private EarthDemon earthD;
    [SerializeField] private AirDemon airD;
    [HideInInspector] public int numDemons;


    /// <summary>
    /// Adds a new answer. Returns true if completed
    /// </summary>
    /// <returns></returns>
    public bool AddAnswer()
    {
        Debug.Log("Monument: Adding Answer");
        answersDone++;
        if (monumentsUnlockAt.Length > 0)
        {
            try
            {
                if (monumentsUnlockAt[currentZoneId + 1] > answersDone)
                {
                    monumentZones[currentZoneId].gameObject.SetActive(false);
                    currentZoneId++;
                    monumentZones[currentZoneId].gameObject.SetActive(true);
                }
            }
            catch (System.Exception e) { }
        }
        //PARTICLES AND SHIT!
        return answersDone >= totalAnswers;
    }

    public void SetSpawning(bool _spawning)
    {
        if (_spawning)
        {
            InvokeRepeating("Spawn", 1, spawnTime);
        }
        else CancelInvoke();
    }

    private void Spawn()
    {
        //tourists = GameObject.FindGameObjectsWithTag("Tourist").ToList();
        //numDemons = GameObject.FindGameObjectsWithTag("Demon").Length;

        Demon _demon = null;
        Transform defHome=null;
        foreach (Transform point in spawnPoint)
        {

            if (numDemons < maxDemons && GameManager.instance.tourists.Count > 0)
            {
                int _random = UnityEngine.Random.Range(0, 2);
                if (_random == 0)
                {
                    _demon = Instantiate(earthD, new Vector3(point.position.x, earthD.transform.position.y, point.position.z), Quaternion.identity);
                    defHome = point;
                }
                else if (_random == 1)
                {

                    _demon = Instantiate(airD, new Vector3(point.position.x, airD.transform.position.y, point .position.z), Quaternion.identity);
                    defHome = point;
                    defHome.position = new Vector3(point.position.x, airD.transform.position.y, point.position.z);
                        }

                _demon.SetHome(defHome);
                _demon.myMonument = this;
                numDemons++;
            }
        }
    }
}
