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
    [SerializeField]private List<GameObject> meshes;
    [SerializeField]private List<Material> materials;

    [SerializeField] GameObject targetPrefab;
    private static readonly int material_IsSelected = Shader.PropertyToID("_IsSelected");

    Renderer[] renderers;
    //[SerializeField] GameObject body;

    void Start()
    {
        targetPrefab = Instantiate(targetPrefab, transform);
        targetPrefab.SetActive(false);

        //renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < meshes.Count; i++)
        {
            if(meshes[i] != null && meshes[i].activeSelf)
           materials.Add(meshes[i].GetComponent<Renderer>().material);
        
        }
    }

    
    void SwitchState(bool selected)
    {
        int selectedValue = 0;
        if (selected) selectedValue = 1;
        
            targetPrefab.SetActive(selected);

            for (int i = 0; i < materials.Count; i++)
            {
               // materials.GetComponent<SkinnedMeshRenderer>().material.SetInt(material_IsSelected,  1);
                materials[i].SetInt(material_IsSelected, selectedValue);

                //materials.Add(meshes[i].GetComponent<Renderer>().material);

            }
        
        
    }

}
