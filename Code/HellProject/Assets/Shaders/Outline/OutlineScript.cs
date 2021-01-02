using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    private GameObject outlineObject;
    [SerializeField] Material outlineMaterial;
    [SerializeField] bool debugShortCuts;
    private Renderer renderer;


    private void Start()
    {
        renderer = CreateOutline(outlineMaterial);
    }

    private void Update()
    {
        if (!debugShortCuts) return;
        if (Input.GetKeyDown(KeyCode.Space))
            SetVisible(!renderer.enabled);
    }

    private Renderer CreateOutline(Material _material)
    {
        outlineObject = Instantiate(new GameObject(), transform.position, transform.rotation, transform);
        outlineObject.AddComponent<MeshFilter>();
        outlineObject.AddComponent<MeshRenderer>();
        outlineObject.GetComponent<MeshFilter>().mesh = GetComponent<MeshFilter>().mesh;

        outlineObject.GetComponent<Renderer>().material = outlineMaterial;
        outlineObject.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<Renderer>().enabled = false;

        return outlineObject.GetComponent<Renderer>();
    }

    public void SetVisible(bool _isVisible)
    {
        renderer.enabled = _isVisible;
    }
}
