using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LevelManager : MonoBehaviour
{
    #region variables
    public bool isQuestionVisible;
    public bool isQuestionable;
    [SerializeField] private List<Monument> monuments;
    [HideInInspector] public Monument currentMonument;

    public static LevelManager instance;

    public EarthDemon earthD;
    public AirDemon airD;
    private int numDemons = 0;
    public List<GameObject> tourists = null;
    private float spawnTime = 2.0f;
    public List<GameObject> spawnPoint;

    public List<GameObject> rocks = null;
    private System.Random r;
    #endregion

    #region methods

    private void Awake()
    {
        if (instance) Destroy(instance);
        instance = this;
        currentMonument = monuments[0];

        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList<GameObject>();

        InvokeRepeating("spawner", 1, spawnTime);
        tourists = GameObject.FindGameObjectsWithTag("Turista").ToList<GameObject>();
        r = new System.Random();

    }

    public void UpdateMonument()
    {
        Debug.Log("Level Manager: Monument Updated!");
    }

    private void spawner()
    {
        numDemons = GameObject.FindGameObjectsWithTag("Demon").Length;
        Demon aux = null;
        foreach (GameObject point in spawnPoint)
        {

            if (numDemons < 3 && tourists.Count > 0)
            {

                int i = (int)UnityEngine.Random.Range(0, 2);
                if (i == 0)
                {
                    aux = Instantiate(earthD, point.transform.position, point.transform.rotation);
                }
                else if (i == 1)
                {
                    
                    aux = Instantiate(airD, new Vector3(point.transform.position.x, 4, point.transform.position.x), point.transform.rotation);

                }
                
                aux.SetHome(point.transform.position);
                numDemons++;
            }

        }
    }
    #endregion
}
