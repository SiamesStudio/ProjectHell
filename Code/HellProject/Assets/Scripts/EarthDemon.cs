using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthDemon : Demon
{

    public void LateUpdate()
    {

        if (attackTourist)
        {
            if (Vector3.Distance(transform.position, home.position) <= 10) AtHome();
           
          
        }

    }
    protected override void ToHome()
    {
        Debug.Log("ToHome" + home.position);
        agent.SetDestination(home.position);
        MoveTourist();

    }
    public void MoveTourist()
    {
       // tourist.transform.position = new Vector3(transform.position.x, 4, transform.position.z);
       // tourist.transform.SetParent(transform);
       tourist.transform.position =tourist.transform.parent.localPosition;
        
        Debug.Log("posicion del demonio" + tourist.transform.position);

    }
    public override void Attack()
    {

        if (tourist && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == true && haveTourist)
        {
            //tourist.gameObject.transform.SetParent(agent.gameObject.transform);
            // tourist.gameObject.transform.parent= this.transform;
            // tourist.transform.position = new Vector3(transform.position.x, 4, transform.position.z);
            tourist.transform.SetParent(transform);
            ToHome();
            attackTourist = true;
        }
        else
        {
            LookingForTourist();
        }
    }
    public override void GoTo()
    {

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



    public override void CollisionDemTou()
    {
        if (tourist && Vector3.Distance(tourist.gameObject.GetComponent<Tourist>().transform.position, transform.position) <= distance)
        {
            collisionT = true;
            tourist.gameObject.GetComponent<Tourist>().SetKidnapped(true);
            haveTourist = true;
            GameManager.instance.tourists.Remove(tourist);


        }
    }
}
