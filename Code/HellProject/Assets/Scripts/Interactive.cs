﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + name);
    }

    //private void Update()
    //{
        //Raycast desde el raton -> iluminar objeto
    //}
}
