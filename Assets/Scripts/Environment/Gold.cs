using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Gold : Obstacleable
{
    [SerializeField] private int amount;

    [SerializeField] private Transform target;
    //[SerializeField] private ParticleSystem

    public Gold()
    {
        interactionTag="Ball";
    }
    internal override void DoAction(Player player)
    {
        if(!GameManager.Instance.isGameEnd)
        {
            EventManager.Broadcast(GameEvent.OnCollectGold);
            transform.DOMove(target.position,0.75f).OnComplete(()=>{
                ScoreManager.Instance.UpdateScore(amount);
                Destroy(gameObject);
            });
        }
    }
}
