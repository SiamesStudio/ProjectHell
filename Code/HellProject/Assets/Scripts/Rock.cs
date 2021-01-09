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
    private Rigidbody rigidBody;
    private bool checkingGrounded = false;

    #endregion

    #region Methods
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(checkingGrounded)
        {
            CheckIfFalling();
        }
    }

    public void RockDown()
    {
        this.gameObject.GetComponent<Collider>().attachedRigidbody.useGravity = true;
        rockDown = true;
        audioSource.clip = fallingSound;
        audioSource.Play();
        StartCoroutine(EnableCheckingGrounded());
    }

    private IEnumerator EnableCheckingGrounded()
    {
        yield return new WaitForSeconds(0.7f);
        checkingGrounded = true;
    }

    private void CheckIfFalling()
    {
        if (rigidBody.velocity.magnitude <= 0.2)
        {
            PlayHitSound();
        }
    }

    public override void PlayHitSound()
    {
        AudioSource.PlayClipAtPoint(hitSound, transform.position, 4);
        Destroy(this.gameObject);
    }

    #endregion
}
