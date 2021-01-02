using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCamera : MonoBehaviour
{

    [SerializeField]
    Transform target;
    float speed;

    private void Start()
    {
        speed = 10f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        //transform.RotateAround(target.position + new Vector3(0,3,0), transform.right, speed);
        transform.RotateAround(target.position + new Vector3(0,8,0), transform.right, -Input.GetAxis("Mouse Y") * speed);
        transform.RotateAround(target.position + new Vector3(0, 8, 0), transform.up, speed * 0.05f);
        //transform.RotateAround(target.position + new Vector3(0, 3, 0), transform.up, -Input.GetAxis("Mouse X") * speed);
        transform.LookAt(target, Vector3.up);
    }
}
