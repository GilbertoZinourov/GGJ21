using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInteractable : MonoBehaviour
{
    public Sprite interactionUI;
    public string interactionText;


    public virtual void Hover()
    {
        Debug.Log("Hovering");
    }

    public virtual void Click()
    {
        Debug.Log("Clicking");
    }
}
