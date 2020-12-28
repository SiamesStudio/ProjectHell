using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class DemonManager : MonoBehaviour
{

    #region Variables

    public List<GameObject> tourists;
    [Header("Spawner")]
    public float spawnTime = 2.0f;
    [SerializeField] private List<GameObject> spawnPoint;
    [Header("Kind of Demons")]
    public EarthDemon earthD;
    public AirDemon airD;
    public int numDemons = 0;
    public static DemonManager instance;
    //DemonManager.instance

    #endregion

    #region Methods
    void Awake()
    {
        //if (instance) Destroy(instance);
        instance = this;
        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList<GameObject>();
        tourists = GameObject.FindGameObjectsWithTag("Tourist").ToList<GameObject>();
    }    

    public void SetSpawning(bool _spawning)
    {
        if(_spawning)
        {
            InvokeRepeating("Spawn", 1, spawnTime);
        }
        else CancelInvoke();
    }

    private void Spawn()
    {
        tourists = GameObject.FindGameObjectsWithTag("Tourist").ToList();
        numDemons = GameObject.FindGameObjectsWithTag("Demon").Length;
        Demon _demon = null;
        foreach (GameObject point in spawnPoint)
        {

            if (numDemons < 3 && tourists.Count > 0 )
            {
                int _random = UnityEngine.Random.Range(0, 2);
                if (_random == 0)
                {
                    _demon = Instantiate(earthD, new Vector3(point.transform.position.x, earthD.transform.position.y, point.transform.position.z), point.transform.rotation);
               
                }
                else if (_random == 1)
                {

                    _demon = Instantiate(airD, new Vector3(point.transform.position.x, airD.transform.position.y, point.transform.position.z), point.transform.rotation);
                  
                }

                _demon.SetHome(point.transform.position);
                numDemons++;
            }
        }
    }

    #endregion
}
