
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

    void Awake()
    {
        if (!TryGetComponent(out agent))
            Debug.LogError("Demon error: NavMeshAgent component not found in " + name);
        agent.speed = velocityToGo;
    }
    public void Update()
    {

        CollisionDemTou();
        if (!collisionT && !haveTourist) GoTo();
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
    protected void AtHome()
    {
        Debug.Log("Estoy  en casa");
        tourist.Die();
        Destroy(this.gameObject);
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
        if (tourist !=null && haveTourist)
        {
            GameManager.instance.tourists.Add(tourist);
            tourist.gameObject.GetComponent<RTSAgent>().isActive = true;
            tourist.SetKidnapped(false);
            tourist.SetTargeted(false);
            tourist.transform.SetParent(null);
            tourist = null;
            haveTourist = false;
            AnimController();

        }
        else
        {
            AnimController();
        }
       
    }
    public void AnimController()
    {
        agent.speed = 0;
        animator.SetBool("die", true);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            Debug.Log("normalizedTime2   " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                Destroy(this.gameObject);
        }
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