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
        //EventManager.Broadcast(GameEvent.OnWall);

        //DURUMA GORE ACARSIN
        player.leaveParticle.Play();
        GameManager.Instance.isWall=true;
    }
}
