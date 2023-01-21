using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Obstacleable
{
    public Wall()
    {
        interactionTag="Ball";
    }

    internal override void DoAction(Player player)
    {
        //SoundManager.Instance.Play("Wall");
    }
}
