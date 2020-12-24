using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthDemon : Demon
{

    public override void Attack()
    {

        if (tourist && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == true && haveTourist)
        {
            newPosition = home;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocityToComeBack / 10);
            tourist.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

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

   

public override void CollisionDemTou()
{
    if (tourist && Vector3.Distance(tourist.gameObject.GetComponent<Tourist>().transform.position, transform.position) <= distance)
    {
            collisionT = true;
            tourist.gameObject.GetComponent<Tourist>().SetKidnapped(true);

            DemonManager.instance.tourists.Remove(tourist);
            haveTourist = true;

        } 
}
}
