
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Demon : Interactive
{
    #region variables 
    public int kind;
    /*[HideInInspector]*/ public Tourist tourist;
    [HideInInspector] public bool haveTourist;
    protected bool collisionT;
    public float distance;
    public Transform home;
    protected Vector3 newPosition;
    public float velocityToGo;
    public float velocityToComeBack;
    [HideInInspector] public Monument myMonument;
    protected NavMeshAgent agent;
    protected bool attackTourist;
    [SerializeField]protected Animator animator;
    //[HideInInspector]

    #endregion

    #region methods

    
    public void Update()
    {
        if (!tourist) LookingForTourist();
        if (tourist.isLeaving) LookingForTourist();
        CollisionDemTou();
        if (!collisionT && !haveTourist && tourist.GetTargeted()) GoTo();
        else if (collisionT && haveTourist && !attackTourist) Attack();
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
    protected virtual void AtHome()
    {
        if(tourist) tourist.Die();
        Destroy(gameObject);
    }

    protected virtual void ToHome() { }

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

    protected void LookingForTourist()
    {
        if (GameManager.instance.touristsAvailable.Count > 0)
        {

            //var visited = new List<Tourist>();
            int i = (int)UnityEngine.Random.Range(0, GameManager.instance.touristsAvailable.Count);
            //while (GameManager.instance.touristsAvailable[i].gameObject.GetComponent<Tourist>().GetKidnapped()
            //    && !visited.Contains(GameManager.instance.touristsAvailable[i]) && visited.Count != GameManager.instance.touristsAvailable.Count)
            //{
            //    i = (int)UnityEngine.Random.Range(0, GameManager.instance.touristsAvailable.Count);
            //    visited.Add(GameManager.instance.touristsAvailable[i]);
            //}
            //if (visited.Count == GameManager.instance.touristsAvailable.Count)
            //{
            //    ToHome();
            //    return;
            //}
            tourist = GameManager.instance.touristsAvailable[i];

            if(tourist)
            {
                if (!tourist.GetTargeted())
                {
                    tourist.gameObject.GetComponent<Tourist>().SetTargeted(true);
                    return;
                    //visited = null;
                }
            }

            ToHome();
        }
        else
        {
            ToHome();
        }

    }
    public override void Interact()
    {
        base.Interact();
        if (GetComponent<NavMeshAgent>()) agent.Stop();
        animator.SetBool("die", true);
        if (tourist != null)
        {
            tourist.SetKidnapped(false);
            tourist.SetTargeted(false);
            tourist.gameObject.GetComponent<RTSAgent>().enabled = true;
            tourist.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            tourist.GetComponent<Collider>().enabled = true;
            tourist.transform.SetParent(null);
            tourist.isQuestionable = true;
            //tourist = null;
            haveTourist = false;

        }

    }
   
    public virtual void CollisionDemTou() { }

    public virtual void PlayFreeingSound() { }
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
    public Transform GetHome()
    {
        return home;
    }
    public void SetHome(Transform home)
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