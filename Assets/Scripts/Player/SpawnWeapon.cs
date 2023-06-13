using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnWeapon : MonoBehaviour
{
    [SerializeField] private Transform Boss;
    [SerializeField] private GameObject Weapon;

    [SerializeField] private List<ParticleSystem> particles;

    public SwordData swordData;

    
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
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }
        GameObject weap=Instantiate(Weapon,new Vector2(0,0),transform.rotation);
        weap.GetComponent<SpriteRenderer>().sprite=swordData.SwordSprite;
        weap.transform.DOMove(Boss.position,0.3f).OnComplete(()=>{
            EventManager.Broadcast(GameEvent.OnHitBoss);
            Destroy(weap);
        });
    }

   
}
