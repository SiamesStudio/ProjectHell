using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System;
using System.Linq;

public class LevelManager : MonoBehaviour
{

    // objeto que quiero spawnear;
    public Demon littleDemon;
    //cada cuanto tiempo
    private float spawnTime = 2.0f;
    // posiciones en las que quiero spawnear
    public List<GameObject> spawnPoint;
    private int numDemons=0;
    public List<GameObject> tourists =null;
    private System.Random r;


    // Start is called before the first frame update
    void Start()
    {
        //busco todos los objetos spawn y los meto en la lista
        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList<GameObject>();
        // el spawn empieza en el segundo 1 y spawnea cada 10 segundo un demonio en cada punto
        //deberiamos poner una condición para que solo haya X demonios en pantalla
        InvokeRepeating("spawner", 1, spawnTime);
        tourists= GameObject.FindGameObjectsWithTag("Turista").ToList<GameObject>();
        r = new System.Random();

        // Update is called once per frame
    }
    void Update()
    {
        
    }

    private void spawner()
    {
        numDemons = GameObject.FindGameObjectsWithTag("Demon").Length;
        foreach (GameObject point in spawnPoint)
        {// creamos un demonio auxiliar que será la instancia.
            if (numDemons < 5 && tourists.Count>0)
            {
                Demon aux = Instantiate(littleDemon, point.transform.position, point.transform.rotation);
                // inicializamos el lugar al que tiene que volver
                aux.SetHome(point.transform.position);
                // iniciamos el turista al que tiene que secuestrar
                //tourists[0].gameObject.GetComponent<Tourist>().SetTargeted(true);
                numDemons ++;
            }
            
        }
    }
/*
    public void selectTourist(Demon d)
    {
        GameObject[] tourists = GameObject.FindGameObjectsWithTag("Turista");
        var visited = new List<GameObject>();
        if (tourists.Length > 0)
        {
            int i = r.Next(tourists.Length);
            while (tourists[i].gameObject.GetComponent<Tourist>().GetKidnapped() && !visited.Contains(tourists[i]) && visited.Count != tourists.Length)
            {
                visited.Add(tourists[i]);
                i = r.Next(tourists.Length);
            }
            if (visited.Count == tourists.Length)
            {
                d.Attack(); //ir a casa sin hacer nada porque todos estan secuestrados
            }
            d.SetTourist(tourists[i]);
            if (d.GetTourist())
                d.GetTourist().gameObject.GetComponent<Tourist>().SetTargeted(true);
        }
        else
        {
            d.Attack();//ir a casa sin hacer nada porque no quedan turistas que secuestrar
        }

    }*/

   
}
