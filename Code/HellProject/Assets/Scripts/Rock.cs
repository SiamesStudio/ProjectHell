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
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void RockDown()
    {
        this.gameObject.GetComponent<Collider>().attachedRigidbody.useGravity = true;
        rockDown = true;
        audioSource.clip = fallingSound;
        audioSource.Play();
    }

        #endregion
}
