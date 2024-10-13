using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum BossAttackType
{
    DarkHole,
    SwordAttack,
    Freeze

}

public class BossAttack : MonoBehaviour
{
    [SerializeField] private ParticleSystem attackingParticle;
    
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnSetMoveZero,OnSetMoveZero);

    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnSetMoveZero,OnSetMoveZero);

    }

    private void OnSetMoveZero()
    {
        transform.DOShakePosition(1,1,10).OnComplete(()=>{
            attackingParticle.Play();
        });
    }
}
