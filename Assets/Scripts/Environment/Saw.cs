using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : Obstacleable
{
    [SerializeField] private ParticleSystem SawParticle;

    public Saw()
    {
        interactionTag="Ball";
    }

    internal override void DoAction(Player player)
    {
        if(!GameManager.Instance.isGameEnd)
        {
            EventManager.Broadcast(GameEvent.OnSaw);
            SawParticle.Play();
        }
        //Buradan handler ile baska yerde fail acariz
    }
}
