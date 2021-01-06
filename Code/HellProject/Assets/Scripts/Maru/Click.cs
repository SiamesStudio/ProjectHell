using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{

    [SerializeField]
    private Transform previousSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Click");
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (objectHit.GetComponent<PointDemon>() != null)
                {
                    Debug.Log("Click on Demon");
                    objectHit.GetComponent<PointDemon>().IsSelected = true;
                    previousSelected.GetComponent<PointDemon>().IsSelected = false;
                    previousSelected = objectHit;
                }
            }
        }
    }
}
