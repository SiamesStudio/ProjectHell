using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    void Start()
    {//Ignore the collisions between layer 0 (default) and layer 8 (custom layer you set in Inspector window)
               
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    //no me funciona
 
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Rock rock))
            Physics.IgnoreCollision(other.gameObject.GetComponent<Rock>().GetComponent<Collider>(), GetComponent<Collider>(), true);
        if (other.gameObject.TryGetComponent(out Tourist _tourist))
        {
            PlayHitSound();
            GameManager.instance.touristsAvailable.Remove(_tourist);
            _tourist.Die();
            _tourist = null;
            Destroy(this.gameObject);
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Rock rock))
        Physics.IgnoreCollision(other.gameObject.GetComponent<Rock>().GetComponent<Collider>(), GetComponent<Collider>(), true);
        if (other.gameObject.TryGetComponent(out Tourist _tourist))
        {
            PlayHitSound();
            GameManager.instance.touristsAvailable.Remove(_tourist);
            _tourist.Die();
            Destroy(this.gameObject);
        }

    }
    
    public virtual void PlayHitSound()
    {

    }

    private void OnDestroy()
    {
        if (!particles) return;
        Instantiate(particles, transform.position, Quaternion.identity);
        
    }

}
