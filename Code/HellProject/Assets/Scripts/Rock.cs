using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Projectile
{
    // Start is called before the first frame update
    #region Variables
    private bool rockDown;

    [Header("Sound")]
    [SerializeField] AudioClip fallingSound;
    [SerializeField] AudioClip hitSound;
    private AudioSource audioSource;

    #endregion

    #region Methods
    void Start()
    {audioSource = GetComponent<AudioSource>();
    }
    
    /*
    void Update()
    {
        instanceShadow.transform.position = new Vector3(this.transform.position.x, shadow.transform.position.y, this.transform.position.z);

        if (rockDown && this.transform.position.y <= 1)
        {
            audioSource.Stop();
            audioSource.clip = hitSound;
            audioSource.Play();
            if (instanceParticles)
            {
                Destroy(this.gameObject);
                Destroy(instanceShadow);
            }else ActiveParticles();
        }

    }
    */
    public void RockDown()
    {
        this.gameObject.GetComponent<Collider>().attachedRigidbody.useGravity = true;
        rockDown = true;
        audioSource.clip = fallingSound;
        audioSource.Play();
    }

        #endregion
}
