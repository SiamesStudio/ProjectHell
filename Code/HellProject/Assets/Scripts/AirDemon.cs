using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDemon : Demon
{

    public GameObject shadow;

    public GameObject rock;
    public bool stopFollowing;
    GameObject instanceShadow;
    GameObject instanceRock;
    void Awake()
    {

        instanceShadow = Instantiate(shadow, new Vector3(transform.position.x, shadow.transform.position.y, transform.position.z), shadow.transform.rotation);

        instanceRock = Instantiate(rock, new Vector3(transform.position.x, 4, transform.position.z), transform.rotation);

    }

    public override void GoTo()
    {

        bool prueba = tourist && tourist.gameObject.GetComponent<Tourist>().GetTargeted() == true && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == false ? true : false;

        if (stopFollowing == false)
        {
            instanceShadow.transform.position = Vector3.Lerp(new Vector3(transform.position.x, 0.2f, transform.position.z), newPosition, Time.deltaTime * velocityToGo / 5);
            instanceRock.transform.position = Vector3.Lerp(new Vector3(transform.position.x, 4, transform.position.z), newPosition, Time.deltaTime * velocityToGo / 5);

        }


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
    public override void Attack()
    {
        if (tourist && collisionT && haveTourist && !tourist.gameObject.GetComponent<Tourist>().GetKidnapped())
        {
            stopFollowing = true;
            RockDown();

        }
        else { ToHome(); }


    }

    public override void CollisionDemTou()
    {


        if (tourist && Vector3.Distance(tourist.transform.position, transform.position) <= distance)
        {
            haveTourist = true;
            collisionT = true;
        }



    }
    public void RockDown()
    {



        instanceRock.transform.position = Vector3.Lerp(instanceRock.transform.position, new Vector3(instanceRock.transform.position.x, 0, instanceRock.transform.position.z), Time.deltaTime * 0.75f);
        if (Vector3.Distance(tourist.transform.position, instanceRock.transform.position) <= 2)
        {
            tourist.gameObject.GetComponent<Tourist>().SetDying(true);

            DemonManager.instance.tourists.Remove(tourist);
            tourist = null;
            ToHome();
            Destroy(instanceRock);
            Destroy(instanceShadow);
        }
        else if (instanceRock.transform.position.y < 1)
        {

            Destroy(instanceRock);
            Destroy(instanceShadow);
            ToHome();
        }


    }


}