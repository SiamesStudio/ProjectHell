using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointExample : PointMovement
{
    [SerializeField] protected Camera myCamera;
    [SerializeField] private SmokeBomb particlesPrefab;
    private SmokeBomb particles;

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
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out RaycastHit _hit))
        {
            particles.transform.position = _hit.point;
            particles.Play();
            Move(_hit.point);
        }
    }
}
