using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearMonsterHandsScript : MonoBehaviour
{
    [SerializeField] private List<MonsterHand> hands = new List<MonsterHand>();

    public void Fear(float val)
    {
        foreach (var hand in hands)
        {
            hand.DoMovement(val);
        }
    }
}
