using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collar : AbstractInteractable
{
    public override void Click()
    {
        SceneManager.LoadScene("End Level");
    }
}
