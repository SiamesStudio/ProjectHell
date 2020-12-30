using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PointMovement
{
    // Si estamos "colisionando" entonces destruimos al demonio, liberar turista y posición, meter turista en lista dle level manager y targeted a falawe
    //tart is called before the first frame update
    //variable para update movimiento
    //
    [SerializeField] protected Camera myCamera;
    [SerializeField] private SmokeBomb particlesPrefab;
    private SmokeBomb particles;

    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask interactiveLayer;
    public Interactive interactive;

    private new void Awake()
    {    base.Awake();
        if (!myCamera) myCamera = Camera.main;
        if(particles) particles = Instantiate(particlesPrefab);

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowRayCast();
            InteractWith();//left
        }

        if (interactive)
        {
            myAgent.SetDestination(interactive.transform.position);
            if(Vector3.Distance(interactive.transform.position, transform.position) <= interactDistance)
            {
                interactive.Interact();
            }
        }
           
    }
     void ThrowRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit _hit))
        {
            if (particles) particles.transform.position = _hit.point;
            if (particles) particles.Play();
            Move(_hit.point);
        }
    }
    void InteractWith()
    {
        //int layerMask = 1 << 8;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit rayHit, Mathf.Infinity, interactiveLayer))
        {
            interactive = rayHit.collider.gameObject.GetComponent<Interactive>();
        }
        else interactive = null;
    }
}