using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMelee : Demon
{
    public Transform touristBag;

    [Header("Sound")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip draggingSound;
    [SerializeField] AudioClip freeingTouristSound;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void LateUpdate()
    {

        if (attackTourist)
        {
            if (Vector3.Distance(transform.position, home.position) <= 3) AtHome();

            if (tourist && attackTourist) MoveTourist();
        }

    }

    protected override void ToHome()
    {
        animator.SetBool("haveTourist", true);
        // agent.speed = velocityToComeBack;
        agent.SetDestination(home.position);
        tourist.transform.parent = touristBag.transform.parent;
        MoveTourist();
    }
    public void MoveTourist()
    {
        tourist.transform.position = touristBag.position;
        tourist.transform.rotation = touristBag.rotation;
        if(!audioSource.isPlaying)
        {
            audioSource.clip = draggingSound;
            audioSource.Play();
        }
    }
    public override void Attack()
    {

        if (tourist && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == true && haveTourist)
        {
            tourist.gameObject.GetComponent<RTSAgent>().isActive = false;
             ToHome();
            attackTourist = true;
        }
        else
        {
            LookingForTourist();
        }
    }
    public override void GoTo()
    {

        bool _hasTarget = tourist && tourist.gameObject.GetComponent<Tourist>().GetTargeted() == true && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == false ? true : false;

        if (_hasTarget)
        {

            newPosition = new Vector3(tourist.transform.position.x, transform.position.y, tourist.transform.position.z);
            agent.SetDestination(newPosition);
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
            haveTourist = true;
            GameManager.instance.tourists.Remove(tourist);


        }
    }

    public override void PlayFreeingSound()
    {
        audioSource.clip = freeingTouristSound;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        audioSource.clip = deathSound;
        audioSource.Play();
    }
}
