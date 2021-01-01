using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthTiles : MonoBehaviour
{
    [Header("Lava direction")]
    [SerializeField] private bool _IsLava;
    [SerializeField] private bool _LavaTop;
    [SerializeField] private bool _LavaRight;
    [SerializeField] private bool _LavaBottom;
    [SerializeField] private bool _LavaLeft;
    [SerializeField] private bool _LavaTopRightCorner;
    [SerializeField] private bool _LavaBottomRightCorner;
    [SerializeField] private bool _LavaBottomLeftCorner;
    [SerializeField] private bool _LavaTopLeftCorner;

    static readonly int material_IsLava = Shader.PropertyToID("_IsLava");
    static readonly int material_LavaTop = Shader.PropertyToID("_LavaTop");
    static readonly int material_LavaRight = Shader.PropertyToID("_LavaRight");
    static readonly int material_LavaBottom = Shader.PropertyToID("_LavaBottom");
    static readonly int material_LavaLeft = Shader.PropertyToID("_LavaLeft");
    static readonly int material_LavaTopRightCorner = Shader.PropertyToID("_LavaTopRightCorner");
    static readonly int material_LavaBottomRightCorner = Shader.PropertyToID("_LavaBottomRightCorner");
    static readonly int material_LavaBottomLeftCorner = Shader.PropertyToID("_LavaBottomLeftCorner");
    static readonly int material_LavaTopLeftCorner = Shader.PropertyToID("_LavaTopLeftCorner");
    static Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;

        if (_IsLava)
        {
            material.SetInt(material_IsLava, 1);
            return;
        }
        if (_LavaTop)               material.SetInt(material_LavaTop, 1);
        if (_LavaRight)             material.SetInt(material_LavaRight, 1);
        if (_LavaBottom)            material.SetInt(material_LavaBottom, 1);
        if (_LavaLeft)              material.SetInt(material_LavaLeft, 1);
        if (_LavaTopRightCorner)    material.SetInt(material_LavaTopRightCorner, 1);
        if (_LavaBottomRightCorner) material.SetInt(material_LavaBottomRightCorner, 1);
        if (_LavaBottomLeftCorner)  material.SetInt(material_LavaBottomLeftCorner, 1);
        if (_LavaTopLeftCorner)     material.SetInt(material_LavaTopLeftCorner, 1);
    }

}
