using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnWeapon : MonoBehaviour
{
    [SerializeField] private Transform Boss;
    [SerializeField] private GameObject Weapon;
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnSpawnWeapon,OnSpawnWeapon);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnSpawnWeapon,OnSpawnWeapon);
    }
    private void OnSpawnWeapon()
    {
        GameObject weap=Instantiate(Weapon,new Vector2(0,0),transform.rotation);
        weap.transform.DOMove(Boss.position,0.5f).OnComplete(()=>{
            EventManager.Broadcast(GameEvent.OnHitBoss);
            Destroy(weap);
        });
    }
}
