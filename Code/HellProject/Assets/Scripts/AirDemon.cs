using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDemon : Demon
{
    public override void Attack()
    {
        // preguntar que hace
        if (tourist)
        {


            tourist.gameObject.GetComponent<Tourist>().SetDying(true);
            tourist = null;
            ToHome();
            Debug.Log("Estoy aquí");
        }
        else { Debug.Log("To home"); ToHome(); }


        }

    public override void collisionDemTou()
    {

        if (tourist && Vector3.Distance(tourist.transform.position, transform.position) <= distance)
        {
            haveTourist = true;
            collisionT = true;
            lm.tourists.Remove(tourist);
        }



    }

}