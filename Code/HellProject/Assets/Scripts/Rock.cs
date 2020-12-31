using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Projectile
{
    // Start is called before the first frame update
    #region Variables
    public GameObject shadow;
    private GameObject instanceShadow;
    [HideInInspector] public Tourist touristR;
    private bool rockDown;

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

        if (rockDown)
            if (touristR && Vector3.Distance(transform.position, touristR.transform.position) <= 1)
            {
                GameManager.instance.tourists.Remove(touristR);
                touristR.SetDying(true);
                touristR = null;
            }
            else if (this.transform.position.y <= 1)
            {
                Destroy(this.gameObject);
                Destroy(instanceShadow);
            }

    }

    public void RockDown()
    {
        this.gameObject.GetComponent<Collider>().attachedRigidbody.useGravity = true;
        rockDown = true;
    }


    #endregion
}
