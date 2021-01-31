using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractDrawer : AbstractInteractable
{
    public virtual bool Clicked()
    {
        return false;
    }
}
