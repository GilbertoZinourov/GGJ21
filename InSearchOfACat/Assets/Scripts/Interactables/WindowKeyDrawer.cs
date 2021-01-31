using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WindowKeyDrawer : AbstractDrawer
{
    [SerializeField] private bool hasKey;
    [SerializeField] private GameObject hideText;
    public Parent _parent; 
    public float time = 5f;
    private bool _inCoroutine = false;

    public override bool Clicked()
    {
        if (!hasKey)
        {
            SpawnParent();
            if(!_inCoroutine)
                StartCoroutine(Hide());
        }

        bool key = hasKey;
        hasKey = false;
        return key;
    }

    private void SpawnParent()
    {
        FindObjectOfType<Parent>().UnlockParent();
    }
    
    private IEnumerator Hide()
    {
        _inCoroutine = true;
        hideText.SetActive(true);
        yield return new WaitForSeconds(time);
        hideText.SetActive(false);
        _inCoroutine = false;
    }
}
