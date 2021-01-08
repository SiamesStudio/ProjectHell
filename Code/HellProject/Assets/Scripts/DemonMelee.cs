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
            if (tourist != null && haveTourist)
            {
                GameManager.instance.tourists.Add(tourist);
                tourist.SetKidnapped(false);
                tourist.SetTargeted(false);
                tourist.gameObject.GetComponent<RTSAgent>().enabled = true;
                tourist.gameObject.GetComponent<NavMeshAgent>().enabled = true;
                tourist.GetComponent<Collider>().enabled = true;
                tourist.transform.SetParent(null);
                tourist = null;
                haveTourist = false;

            }
            animator.SetBool("default", true);
            Destroy(this.gameObject);
        }

    }

    protected override void ToHome()
    {
        Debug.Log("ToHome");
        animator.SetBool("haveTourist", true);

        tourist.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        agent.SetDestination(home.position);
        tourist.transform.position = touristBag.position;
        tourist.transform.rotation = touristBag.rotation;
        tourist.transform.parent = touristBag.transform.parent;
        tourist.GetComponent<Collider>().enabled = false;
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
            Debug.Log("atack");
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
        if (myMonument) myMonument.numDemons--;
        audioSource.clip = deathSound;
        audioSource.Play();
    }
}
