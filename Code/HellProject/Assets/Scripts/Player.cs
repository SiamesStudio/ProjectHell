using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : PointMovement
{
    // Si estamos "colisionando" entonces destruimos al demonio, liberar turista y posición, meter turista en lista dle level manager y targeted a falawe
    //tart is called before the first frame update
    //variable para update movimiento
    //
    [SerializeField] Animator anim;
    [SerializeField] protected Camera myCamera;
    [SerializeField] private PointAnimation particlesPrefab;
    private PointAnimation particles;

    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask interactiveLayer;
    public Interactive interactive;

    //UI RAYCAST
    [SerializeField] private GraphicRaycaster[] m_Raycaster;
    [SerializeField] private EventSystem[] m_EventSystem;
    private PointerEventData m_PointerEventData;

    private new void Awake()
    {    
        base.Awake();
        if (!myCamera) myCamera = Camera.main;
        particles = Instantiate(particlesPrefab);
        particles.name = "PointAnimation";
    }


    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isWalking", Vector3.Distance(myAgent.destination, transform.position) > 0.1); ;
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckUI()) return;
            ThrowRayCast();
            InteractWith();//left
        }

        if (interactive)
        {
            myAgent.SetDestination(interactive.transform.position);
            if(Vector3.Distance(interactive.transform.position, transform.position) <= interactDistance)
            {
                interactive.Interact();
                anim.SetTrigger("Attack");
            }
        }
           
    }

    private bool CheckUI()
    {
        for (int i = 0; i < m_Raycaster.Length; i++)
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem[i]);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> _results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster[i].Raycast(m_PointerEventData, _results);
            if (_results.Count > 0) return true;
        }

        return false;
    }
    void ThrowRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit _hit))
        {
            if(interactive != null)interactive.GetComponent<PointDemon>().IsSelected = false;
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
            if(interactive != null)interactive.GetComponent<PointDemon>().IsSelected = false;
            interactive = rayHit.collider.gameObject.GetComponent<Interactive>();
            interactive.GetComponent<PointDemon>().IsSelected = true;
        }
        else interactive = null;
    }
}