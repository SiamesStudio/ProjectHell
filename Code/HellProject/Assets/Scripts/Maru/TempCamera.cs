using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCamera : MonoBehaviour
{

    [SerializeField]
    Transform target;
    float speed;
    [SerializeField]
    private Transform previousSelected;

    private void Start()
    {
        speed = 10f;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    void Update()
    {
       //transform.RotateAround(target.position + new Vector3(0,3,0), transform.right, speed);
        //transform.RotateAround(target.position + new Vector3(0, 8, 0), transform.up, speed * 0.05f);



       //transform.RotateAround(target.position + new Vector3(0,8,0), transform.right, -Input.GetAxis("Mouse Y") * speed);
       //transform.RotateAround(target.position + new Vector3(0, 3, 0), transform.up, -Input.GetAxis("Mouse X") * speed);
        transform.LookAt(target, Vector3.up);



        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Click");
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                Debug.Log("Click on: " + objectHit.name);// + 
                if (objectHit.GetComponent<PointDemon>() != null)
                {
                    
                    objectHit.GetComponent<PointDemon>().IsSelected = true;
                    if (previousSelected != null && previousSelected != objectHit && previousSelected.GetComponent<PointDemon>() != null)
                    {
                        
                        previousSelected.GetComponent<PointDemon>().IsSelected = false;
                    }
                }
                if (previousSelected != null)Debug.Log("Previous selected: " + previousSelected.name);
                previousSelected = objectHit;
            }
        }
    }


}
