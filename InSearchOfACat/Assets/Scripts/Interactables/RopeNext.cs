using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeNext : AbstractInteractable
{
    [SerializeField] private GameObject prevRoom, nextRoom, spawnPoint;
    public bool enableMovement, needKey;
    private MouseInteraction _player;

    private void Awake()
    {
        _player = FindObjectOfType<MouseInteraction>();
    }

    public override void Click()
    {
        var cam = FindObjectOfType<CameraMovement>();
        cam.camMovementEnabled = enableMovement;
        if (!enableMovement)
        {
            cam.transform.position = new Vector3(0, 0, -10);
        }
        prevRoom.SetActive(false);
        nextRoom.SetActive(true);
        _player.transform.position = spawnPoint.transform.position;
    }
}
