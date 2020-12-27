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
    void Update()
    {

    }

    

    public void AuxMethod( int inOut)
    {
        if(inOut==0)
        {
            InvokeRepeating("Spawner", 1, spawnTime);
        }
        else if (inOut == 1) CancelInvoke();



        Debug.Log("Aquí quiero meter la llamada al spawner");
        


    }
    private void Spawner()
    {
        tourists = GameObject.FindGameObjectsWithTag("Tourist").ToList();
        numDemons = GameObject.FindGameObjectsWithTag("Demon").Length;
        Demon aux = null;
                foreach (GameObject point in spawnPoint)
        {

            if (numDemons < 3 && tourists.Count > 0 )
            {

                int i = (int)UnityEngine.Random.Range(0, 2);
                if (i == 0)
                {
                    aux = Instantiate(earthD, new Vector3(point.transform.position.x, earthD.transform.position.y, point.transform.position.z), point.transform.rotation);
               
                }
                else if (i == 1)
                {

                    aux = Instantiate(airD, new Vector3(point.transform.position.x, airD.transform.position.y, point.transform.position.z), point.transform.rotation);
                  
                }

                aux.SetHome(point.transform.position);
                numDemons++;
            }

        }
    }

    #endregion
}
