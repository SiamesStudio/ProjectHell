using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    //no me funciona
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Tourist>())
        {
            other.enabled = true;
        }
       else if (other.gameObject.GetComponent<Tourist>())
        {
            
        }
    }

}
