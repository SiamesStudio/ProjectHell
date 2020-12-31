using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [HideInInspector] public Tourist touristR;
    public Transform point4Particles;
    void Start()
    {//Ignore the collisions between layer 0 (default) and layer 8 (custom layer you set in Inspector window)

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    //no me funciona
 
    private void OnCollisionEnter(Collision other)
    {/*
        Debug.Log("Other" + other.gameObject.ToString());

        if (other.gameObject.GetComponent<RTSAgent>())
        {
            GameManager.instance.tourists.Remove(touristR);
            touristR.SetDying(true);
            touristR = null;
        }*/
        
    }

}
