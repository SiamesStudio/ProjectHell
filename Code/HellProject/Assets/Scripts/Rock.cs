using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Projectile
{
    // Start is called before the first frame update
    #region Variables
    public GameObject shadow;
    private GameObject instanceShadow;
    private bool rockDown;
    public ParticleSystem particles;
    private ParticleSystem instanceParticles;
    #endregion
    #region Methods
    void Start()
    {

        instanceShadow = Instantiate(shadow, new Vector3(transform.position.x, shadow.transform.position.y, transform.position.z), shadow.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        instanceShadow.transform.position = new Vector3(this.transform.position.x, shadow.transform.position.y, this.transform.position.z);

        if (rockDown && this.transform.position.y <= 1)
            {
            
            if (instanceParticles)
            {
                 Destroy(this.gameObject);
                Destroy(instanceShadow);
            }else ActiveParticles();
        }

    }
  
    public void RockDown()
    {
        this.gameObject.GetComponent<Collider>().attachedRigidbody.useGravity = true;
        rockDown = true;

    }

    public void ActiveParticles()
    {

        instanceParticles =Instantiate(particles, touristR.transform.position, transform.rotation);
     
    }
    #endregion
}
