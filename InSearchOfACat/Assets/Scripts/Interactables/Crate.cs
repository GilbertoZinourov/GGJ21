using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : AbstractInteractable
{
    public bool picked;
    private Camera _cam;
    private Transform _crate;
    private float _z, _y;
    private MouseInteraction _mouse;

    private void Awake()
    {
        _cam = Camera.main;
        _crate = transform.parent;
        _z = _crate.position.z;
        _y = _crate.position.y;
        _mouse = FindObjectOfType<MouseInteraction>();
    }

    private void Update()
    {
        if (picked && Input.GetMouseButtonUp(0))
        {
            PutDown();
        }

        if (Vector3.Distance(transform.position, _mouse.transform.position) >= _mouse.actionRange)
        {
            PutDown();
        }
    }

    public void PutDown()
    {
        picked = false;
    }

    private void FixedUpdate()
    {
        if (picked)
        {
            Vector3 pos = _cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = _z;
            pos.y = _y;
            _crate.position = pos;
        }
    }

    public override void Click()
    {
        picked = true;
    }
}
