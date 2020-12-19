
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static LevelManager;

public class Demon : MonoBehaviour
{
    #region variables 
    public GameObject tourist; // solo puede tener un turista
    private bool haveTourist;
    private GameObject collisionT;
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
        collisionT = null;
        tourist = null;
        lm = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tourist)
        {
            LookingForTourist();
        }
        else
        {
            if (!collisionT) GoTo();
            else if (collisionT) Attack();
        }

    }
    //metodo nuevo que destruye al demonio cuando llega a su posición inicial ya que sino hay muchos
    void atHome()
    {
        Destroy(this.gameObject);
    }
    void toHome()
    {

        newPosition = home;
        tourist = null;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocity / 5);
        if (Vector3.Distance(home, transform.position) <= distance) atHome();
    }
    public void Attack()
    {

        if (tourist && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == true && haveTourist)
        {
            newPosition = home;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocity / 5);
            tourist.transform.position = new Vector3(transform.position.x, 4, transform.position.z);

            if (Vector3.Distance(home, transform.position) <= distance)
            {
                tourist.gameObject.GetComponent<Tourist>().SetDying(true);
                tourist = null;
                atHome();

            }
        }
        else
        {
            toHome();
        }

    }
    void GoTo()
    {

        if (tourist && tourist.gameObject.GetComponent<Tourist>().GetTargeted() == true && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == false)
        {
            newPosition = new Vector3(tourist.transform.position.x, transform.position.y, tourist.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocity / 5);


        }
        else
        {
            newPosition = transform.position;
            tourist = null;
            LookingForTourist();

        }


    }

    void LookingForTourist()
    {
        var visited = new List<GameObject>();
        if (lm.tourists.Count > 0)
        {
            int i = (int)UnityEngine.Random.Range(0, lm.tourists.Count);
            while (lm.tourists[i].gameObject.GetComponent<Tourist>().GetKidnapped() && !visited.Contains(lm.tourists[i]) && visited.Count != lm.tourists.Count)
            {
                visited.Add(lm.tourists[i]);
                i = (int)UnityEngine.Random.Range(0, lm.tourists.Count);
            }
            if (visited.Count == lm.tourists.Count)
            {
                toHome();
            }
            tourist = lm.tourists[i];
            if (tourist)
                tourist.gameObject.GetComponent<Tourist>().SetTargeted(true);
        }
        else
        {
            Attack();
        }


    }
    void OnCollisionEnter(Collision collision)
    {
        if (!collisionT && collision.gameObject == tourist && !tourist.gameObject.GetComponent<Tourist>().GetKidnapped())
        {
            collisionT = collision.gameObject;
            tourist.gameObject.GetComponent<Tourist>().SetKidnapped(true);
            lm.tourists.Remove(tourist);
            haveTourist = true;
        }
        else if (!collisionT && collision.gameObject)
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
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
    public Vector3 GetHome()
    {
        return home;
    }
    public void SetHome(Vector3 home)
    {
        this.home = home;
    }
    #endregion
}