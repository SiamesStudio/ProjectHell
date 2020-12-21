
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static LevelManager;

public class Demon : MonoBehaviour
{
    #region variables 
    public int kind;
    public GameObject tourist;
    public bool haveTourist;
    protected bool collisionT;
    public float distance;
    protected Vector3 home = Vector3.zero;
    protected Vector3 newPosition;
    public float velocity ;
    public LevelManager lm;
    #endregion
    #region methods
     public void Start()
    {
        newPosition = transform.position;
        collisionT = false;
        haveTourist = false;
        tourist = null;
        lm = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
        if (!tourist)
        {

            LookingForTourist();
        }
    }

    // Update is called once per frame
    public void Update()
    {

        collisionDemTou();

        if (!collisionT) GoTo();
        else if (collisionT) Attack();



    }
    protected void AtHome()
    {
        Destroy(this.gameObject);
    }

    protected void ToHome()
    {

        newPosition = home;
        tourist = null;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocity / 5);
        if (Vector3.Distance(home, transform.position) <= distance) AtHome();
    }

    public virtual void Attack() { }

    public virtual void GoTo() {

        bool prueba = tourist && tourist.gameObject.GetComponent<Tourist>().GetTargeted() == true && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == false ? true : false;

        if (prueba)
        {
            newPosition = new Vector3(tourist.transform.position.x, transform.position.y, tourist.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocity / 5);
        }
        else
        {
            LookingForTourist();
            
        }
    }

    protected  void LookingForTourist()
    {
        
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
            {
                tourist.gameObject.GetComponent<Tourist>().SetTargeted(true);
                visited = null;
            }
        }
        else
        {
            ToHome();
        }


    }
    public virtual void collisionDemTou() { }
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