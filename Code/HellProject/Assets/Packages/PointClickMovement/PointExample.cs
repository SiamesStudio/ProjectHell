using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PointExample : PointMovement
{
    [SerializeField] protected Camera myCamera;
    [SerializeField] private SmokeBomb particlesPrefab;
    private SmokeBomb particles;

    //UI RAYCAST
    [SerializeField] private GraphicRaycaster[] m_Raycaster;
    [SerializeField] private EventSystem[] m_EventSystem;
    private PointerEventData m_PointerEventData;
    //[SerializeField] LayerMask layerUI;

    private new void Awake()
    {
        base.Awake();
        if (!myCamera) myCamera = Camera.main;
        particles = Instantiate(particlesPrefab);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) ThrowRayCast();
    }
    private void ThrowRayCast()
    {
        if (CheckUI()) return;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out RaycastHit _hit))
        {
            particles.transform.position = _hit.point;
            particles.Play();
            Move(_hit.point);
        }
    }

    private bool CheckUI()
    {
        for(int i = 0; i < m_Raycaster.Length; i++)
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
}
