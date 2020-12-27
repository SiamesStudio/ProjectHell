
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Demon : MonoBehaviour
{
    #region variables 
    public int kind;
    public GameObject tourist;
    public bool haveTourist;
    protected bool collisionT;
    public float distance;
    public Vector3 home = Vector3.zero;
    protected Vector3 newPosition;
    public float velocityToGo;
    public float velocityToComeBack;
    
    #endregion
    #region methods
     public void Start()
    {
        newPosition = transform.position;
        collisionT = false;
        haveTourist = false;
        tourist = null;
     
        if (!tourist)
        {

            LookingForTourist();
        }
    }
    public void Update()
    {

        CollisionDemTou();
        if (!collisionT && !haveTourist) GoTo();
        else if (collisionT && haveTourist) Attack();
        
    }
    protected void AtHome()
    {
        Destroy(this.gameObject);
    }

    protected void ToHome()
    {

        newPosition = home;
        tourist = null;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocityToComeBack / 5);
        if (Vector3.Distance(home, transform.position) <= distance) AtHome();
    }

    public virtual void Attack() { }

    public virtual void GoTo() {

        bool prueba = tourist && tourist.gameObject.GetComponent<Tourist>().GetTargeted() == true && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == false ? true : false;

        if (prueba)
        {
            newPosition = new Vector3(tourist.transform.position.x, transform.position.y, tourist.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocityToGo / 5);
        }
        else
        {
            LookingForTourist();
            
        }
    }

    protected  void LookingForTourist()
    {
        if (DemonManager.instance.tourists.Count > 0)
        {

            var visited = new List<GameObject>();
            int i = (int)UnityEngine.Random.Range(0, DemonManager.instance.tourists.Count);
            while (DemonManager.instance.tourists.Count > 0 && DemonManager.instance.tourists[i].gameObject.GetComponent<Tourist>().GetKidnapped() && !visited.Contains(DemonManager.instance.tourists[i]) && visited.Count != DemonManager.instance.tourists.Count)
            {
                i = (int)UnityEngine.Random.Range(0, DemonManager.instance.tourists.Count);
                visited.Add(DemonManager.instance.tourists[i]);
            }
            if (visited.Count == DemonManager.instance.tourists.Count)
            {
                ToHome();
            }
            tourist = DemonManager.instance.tourists[i];

            if (tourist)
            {
                tourist.gameObject.GetComponent<Tourist>().SetTargeted(true);
                visited = null;
            }
            else ToHome();
        }
        else
        {
            ToHome();
        }


    }
    public virtual void CollisionDemTou() { }
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