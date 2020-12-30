
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Demon : Interactive
{
    #region variables 
    public int kind;
    [HideInInspector] public Tourist tourist;
    [HideInInspector] public bool haveTourist;
    protected bool collisionT;
    public float distance;
    [HideInInspector] public Vector3 home = Vector3.zero;
    protected Vector3 newPosition;
    public float velocityToGo;
    public float velocityToComeBack;
    [HideInInspector] public Monument myMonument;
    private NavMeshAgent agent;
    
    #endregion
    #region methods

    void Awake()
    {
        if (!TryGetComponent(out agent))
            Debug.LogError("Demon error: NavMeshAgent component not found in " + name);
    }
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
        agent.SetDestination(home);
        if (Vector3.Distance(home, transform.position) <= distance) AtHome();
        tourist = null;
    }

    public virtual void Attack() { }

    public virtual void GoTo() {

        bool _hasTarget = tourist && tourist.gameObject.GetComponent<Tourist>().GetTargeted() == true && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == false ? true : false;

        if (_hasTarget)
        {
            newPosition = new Vector3(tourist.transform.position.x, transform.position.y, tourist.transform.position.z);
            agent.SetDestination(newPosition);
        }
        else
        {
            LookingForTourist();          
        }
    }

    protected  void LookingForTourist()
    {
        if (GameManager.instance.tourists.Count > 0)
        {

            var visited = new List<Tourist>();
            int i = (int)UnityEngine.Random.Range(0, GameManager.instance.tourists.Count);
            while (GameManager.instance.tourists.Count > 0 
                && GameManager.instance.tourists[i].gameObject.GetComponent<Tourist>().GetKidnapped() 
                && !visited.Contains(GameManager.instance.tourists[i]) && visited.Count != GameManager.instance.tourists.Count)
            {
                i = (int)UnityEngine.Random.Range(0, GameManager.instance.tourists.Count);
                visited.Add(GameManager.instance.tourists[i]);
            }
            if (visited.Count == GameManager.instance.tourists.Count)
            {
                ToHome();
            }
            tourist = GameManager.instance.tourists[i];

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

    public override void Interact()
    {
        base.Interact();
        if (tourist && haveTourist)
        {
            tourist.SetKidnapped(false);
            tourist.SetTargeted(false);
            GameManager.instance.tourists.Add(tourist);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (myMonument) myMonument.numDemons--;
    }
    public virtual void CollisionDemTou() { }
    #endregion
    #region getters and setters
    public Tourist GetTourist()
    {
        return tourist;
    }
    public void SetTourist(Tourist tourist)
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