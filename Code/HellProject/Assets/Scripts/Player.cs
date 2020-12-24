using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PointMovement
{
    // Si estamos "colisionando" entonces destruimos al demonio, liberar turista y posición, meter turista en lista dle level manager y targeted a falawe
    //tart is called before the first frame update
    //variable para update movimiento
    //
    GameObject collisionObject;
    [SerializeField] protected Camera myCamera;
    [SerializeField] private SmokeBomb particlesPrefab;
    private SmokeBomb particles;
    private bool targeted;

    [SerializeField] private float distance;

    


    Ray ray;
    RaycastHit rayHit;
    private new void Awake()
    {    base.Awake();
        targeted = false;
        if (!myCamera) myCamera = Camera.main;
        particles = Instantiate(particlesPrefab);

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) InteractWith();//left
        if (targeted && collisionObject)
        {
            Move(collisionObject.transform.position);
            if(Vector3.Distance(collisionObject.transform.position, transform.position) <= distance)
            {
                AttackDemon(collisionObject);
            }
        }
           
    }
     void ThrowRayCast()
    {
        if (Physics.Raycast(ray, out RaycastHit _hit))
        {
            particles.transform.position = _hit.point;
            particles.Play();
            Move(_hit.point);
            targeted = false;

        }
    }
    void InteractWith()
    {
        int layerMask = 1 << 8;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, layerMask))
        {
            collisionObject = rayHit.collider.gameObject;

            switch (collisionObject.tag)
            {
                case "Demon":
                    targeted = true;

                    break;
            }
        }
        else ThrowRayCast();
    }
    private void AttackDemon(GameObject demon)
    {
        
        GameObject aux = demon.gameObject.GetComponent<Demon>().tourist;
        if (aux && demon.gameObject.GetComponent<Demon>().haveTourist)
        {
            aux.gameObject.GetComponent<Tourist>().SetKidnapped(false);
            aux.gameObject.GetComponent<Tourist>().SetTargeted(false);
            DemonManager.instance.tourists.Add(aux);
        }
        Destroy(demon);

    }
    //metodo pegar solo si está cerca y lo destruye. 
}