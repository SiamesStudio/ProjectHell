using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 locaslPos;

    private void Awake()
    {
        locaslPos = target.position - transform.position;
        
    }

    void Update()
    {
        transform.position = target.position - locaslPos;
    }
}
