using UnityEngine;

public class HideSpot : AbstractInteractable
{
    public override void Click()
    {
        GameObject player = FindObjectOfType<MouseInteraction>().gameObject;
        player.transform.position =
            new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
    }
}
