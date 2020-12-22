using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointExample : PointMovement
{
    [SerializeField] protected Camera myCamera;
    [SerializeField] private SmokeBomb particlesPrefab;
    private SmokeBomb particles;
    [SerializeField] LayerMask layerUI;

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
        return false;
        /*
        Ray2D _ray = Camera.main.Screen(Input.mousePosition);
        if(Physics.Raycast(_ray, out RaycastHit _hit, layerUI))
            Debug.Log(_hit.collider.gameObject);
        return false;
        */
    }
}
