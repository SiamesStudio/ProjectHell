using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDemon : MonoBehaviour
{



    private bool isSelected;
    [SerializeField]
    public bool IsSelected {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;
            SwitchState(value);
        }
    }


    [SerializeField] GameObject targetPrefab;
    private static readonly int material_IsSelected = Shader.PropertyToID("_IsSelected");

    Renderer[] renderers;
    [SerializeField] GameObject body;

    void Start()
    {
        targetPrefab = Instantiate(targetPrefab, transform);
        targetPrefab.SetActive(false);

        //renderers = GetComponentsInChildren<Renderer>();
        //for (int i = 0; i < renderers.Length; i++)
        //{
        //   //renderers[i].material = new Material()
        //
        //}
    }

    
    void SwitchState(bool selected)
    {
        if (isSelected)
        {
            targetPrefab.SetActive(selected);
            body.GetComponent<SkinnedMeshRenderer>().material.SetInt(material_IsSelected,  1);
        }
        
    }

}
