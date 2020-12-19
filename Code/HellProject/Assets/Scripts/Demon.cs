
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static LevelManager;

public class Demon : MonoBehaviour
{
    #region variables 
    public int kind;
    public GameObject tourist; // solo puede tener un turista
     //public GameObject rock;
    private bool rockToTourist;
    private bool collisionT;
    public float distance;
    private Vector3 home = Vector3.zero;
    private Vector3 newPosition;
    public float velocity;
    private LevelManager lm;
    #endregion
    #region methods
    void Start()
    {
        newPosition = transform.position;
        collisionT = false;
        tourist = null;
        lm = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
        if (!tourist)
        {

            LookingForTourist();
        }
    }

    // Update is called once per frame
    void Update()
    {

        collisionDemTou();
        /* pwta
                if (!tourist)
                {
                    LookingForTourist();
                }
                else
                {*/
        if (!collisionT) GoTo();
        else if (collisionT) Attack();
        if (!rockToTourist) FlyTo();
        else if (rockToTourist) FlyAttack();

        // }


    }
    void AtHome()
    {
        Destroy(this.gameObject);
    }

    void ToHome()
    {

        newPosition = home;
        tourist = null;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocity / 5);
        if (Vector3.Distance(home, transform.position) <= distance) AtHome();
    }

    public void Attack()
    {
        if (kind == 0)
        {
            if (tourist && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == true)
            {
                newPosition = home;
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocity / 5);
                tourist.transform.position = new Vector3(transform.position.x, 4, transform.position.z);

                if (Vector3.Distance(home, transform.position) <= distance)
                {
                    tourist.gameObject.GetComponent<Tourist>().SetDying(true);
                    tourist = null;
                    AtHome();

                }
            }
            else
            {
                LookingForTourist();
            }
        }
    }
    public void FlyAttack()
    {
        if (kind == 1)
        {

            Debug.Log("Soy demon2 y estoy atacando");
            
        }
    }
    void GoTo()
    {
        bool prueba = tourist && tourist.gameObject.GetComponent<Tourist>().GetTargeted() == true && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == false ? true : false;
        if (kind == 0)
        {
            if (prueba)
            {
                newPosition = new Vector3(tourist.transform.position.x, transform.position.y, tourist.transform.position.z);
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocity / 5);
            }
            
        }
    }
    void FlyTo()
    {

        if (tourist)
        {
            Debug.Log("Go To Tourist");
            newPosition = new Vector3(tourist.transform.position.x, transform.position.y, tourist.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocity / 5);
           
        }
        else if (!tourist)
        {
            //hay que hacer un nuevo looking for tourist para el demonio 2(hacer herencia )
            Debug.Log("no hay turista y soy 2");
            
        }
    }


    void LookingForTourist()
    {
        // hace que explote todo por stack overflow
        if (lm.tourists.Count > 0)
        {

            var visited = new List<GameObject>();
            int i = (int)UnityEngine.Random.Range(0, lm.tourists.Count);
            while (lm.tourists[i].gameObject.GetComponent<Tourist>().GetKidnapped() && !visited.Contains(lm.tourists[i]) && visited.Count != lm.tourists.Count)
            {
                i = (int)UnityEngine.Random.Range(0, lm.tourists.Count);
                visited.Add(lm.tourists[i]);
            }
            if (visited.Count == lm.tourists.Count)
            {
                ToHome();
            }
            tourist = lm.tourists[i];
            if (tourist)
            { tourist.gameObject.GetComponent<Tourist>().SetTargeted(true);
                visited = null;
            }
        }
        else
        {
            ToHome();
        }


    }
   void collisionDemTou()
    {
        if (kind == 0)
        {
            if (tourist && Vector3.Distance(tourist.gameObject.GetComponent<Tourist>().transform.position, transform.position) <= distance)
            {
                {
                    collisionT = true;
                    tourist.gameObject.GetComponent<Tourist>().SetKidnapped(true);
                    lm.tourists.Remove(tourist);
                }
            }
        }
        if (kind == 0)
        {
            if(tourist && Vector3.Distance(tourist.transform.position, transform.position) <= distance)

            rockToTourist = true;

        }

    }

    #endregion
    #region getters and setters
    public GameObject GetTourist()
    {
        return tourist;
    }
    public void SetTourist(GameObject tourist)
    {
        this.tourist = tourist;
    }
    /*public GameObject GetRock()
    {
        return rock;
    }
    public void SetRock(GameObject rock)
    {
        this.rock = rock;
    }*/
    public Vector3 GetHome()
    {
        return home;
    }
    public void SetHome(Vector3 home)
    {
        this.home = home;
    }
    public int GetKind()
    {
        return kind;
    }
    public void SetKind(int kind)
    {
        this.kind = kind;
    }
    #endregion
}