using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonMelee : Demon
{

    
    public Transform touristBag;

    [Header("Sound")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip draggingSound;
    [SerializeField] AudioClip freeingTouristSound;
    private AudioSource audioSource;

    void Awake()
    {
        if (!TryGetComponent(out agent))
            Debug.LogError("Demon error: NavMeshAgent component not found in " + name);
        agent.speed = velocityToGo;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //InvokeRepeating("LookingForTourist", 1f, 1f); 
    }

    public void LateUpdate()
    {       
        if (attackTourist)
        {
            if (Vector3.Distance(transform.position, home.position) <= 5) AtHome();

            if (tourist && attackTourist) MoveTouristSound();
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die") && (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f))
        {

            animator.SetBool("default", true);
            Destroy(gameObject);
        }

        //LookingForTourist();

    }

    protected override void ToHome()
    {
        Debug.Log("ToHome");
        animator.SetBool("haveTourist", true);

        agent.SetDestination(home.position);

    }
    public void MoveTouristSound()
    {
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
            tourist.gameObject.GetComponent<RTSAgent>().enabled = false;
            tourist.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            tourist.transform.position = touristBag.position;
            tourist.transform.rotation = touristBag.rotation;
            tourist.transform.parent = touristBag.transform.parent;
            tourist.GetComponent<Collider>().enabled = false;
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
        if (tourist && Vector3.Distance(tourist.gameObject.GetComponent<Tourist>().transform.position, transform.position) <= distance && tourist.GetTargeted())
        {
            collisionT = true;
            tourist.gameObject.GetComponent<Tourist>().SetKidnapped(true);
            haveTourist = true;
        }
    }

    public override void PlayFreeingSound()
    {
        audioSource.clip = freeingTouristSound;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        if (myMonument) myMonument.numDemons--;
        audioSource.clip = deathSound;
        audioSource.Play();
        if (tourist) tourist.transform.parent = null;
    }
}
