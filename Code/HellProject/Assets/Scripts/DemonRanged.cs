using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonRanged : Demon
{


    public Rock rock;
    [HideInInspector] public bool stopFollowing;
    private Rock instanceRock;
    public Transform rockBag;

    [Header("Sound")]
    [SerializeField] AudioClip deathSound;
    private AudioSource audioSource;

    void Start()
    {
        instanceRock = Instantiate(rock, rockBag.position, transform.rotation);
        audioSource = GetComponent<AudioSource>();

    }
    public void LateUpdate()
    {

        if (attackTourist)
        {
            if (Vector3.Distance(transform.position, home.position) <= 10) AtHome();
            ToHome();
        }

    }


    protected override void AtHome()
    {
        Destroy(gameObject);
    }

    protected override void ToHome()
    {
        newPosition = home.position;
        this.transform.LookAt(home.transform);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocityToGo);

    }

    public override void GoTo()
    {
        
        bool condition = tourist && tourist.gameObject.GetComponent<Tourist>().GetTargeted() == true && tourist.gameObject.GetComponent<Tourist>().GetKidnapped() == false ? true : false;

        
        if (condition)
        {
            this.transform.LookAt(new Vector3(tourist.transform.position.x, transform.position.y,tourist.transform.position.z));

            newPosition = new Vector3(tourist.transform.position.x, transform.position.y, tourist.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * velocityToGo / 5);
            instanceRock.transform.position = Vector3.Lerp(rockBag.position, newPosition, Time.deltaTime * velocityToGo / 5);

        }
        else
        {

            this.transform.LookAt(home);
            LookingForTourist();

        }
    }
    public override void Attack()
    {
        if (tourist && collisionT && haveTourist && !tourist.gameObject.GetComponent<Tourist>().GetKidnapped())
        {

            instanceRock.RockDown();

            animator.SetBool("withRock", true);

            attackTourist = true;

            tourist = null;
            transform.LookAt(home);

            ToHome();

            attackTourist = true;
        }
        else { tourist = null; ToHome(); }
    }

    public override void CollisionDemTou()
    {

        if (tourist && 
            Vector2.Distance((new Vector2(tourist.transform.position.x, tourist.transform.position.z)), (new Vector2(transform.position.x, transform.position.z))) <= 0.3 &&
                tourist.GetTargeted())
        {
            haveTourist = true;
            collisionT = true;
        }

    }

    private void OnDestroy()
    {
        if (myMonument) myMonument.numDemons--;
        //audioSource.clip = deathSound;
        //audioSource.Play();
    }
}