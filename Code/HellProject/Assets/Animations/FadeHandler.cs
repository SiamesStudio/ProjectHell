using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.instance.iterations < 1) return;
        GameManager.instance.animator.SetTrigger("isLoaded");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
