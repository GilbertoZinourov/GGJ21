using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : AbstractInteractable
{
    public override void Click()
    {
        Destroy(gameObject);
    }
}
