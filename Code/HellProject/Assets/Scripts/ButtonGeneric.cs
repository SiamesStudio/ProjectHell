using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonGeneric : MonoBehaviour
{

    [Header("Botones ")]
    public Button buttonGeneric1;
    public Button buttonGeneric2;
    public Button buttonGeneric3;

    [Header("Textos")]
    public Text button1;
    public Text button2;
    public Text button3;

    [Header("Imagenes")]
    public Image imagen1;
    public Image imagen2;
    public Image imagen3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void ButtonPressed()
    {
        
        Debug.Log("He presionado el capitulo");

    }
    public virtual void SetButton1() { }
    public virtual void SetButton2() { }
    public virtual void SetButton3() { }
}
