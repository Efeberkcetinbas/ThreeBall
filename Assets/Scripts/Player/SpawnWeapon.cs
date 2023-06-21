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
            particles[i].transform.position=swordData.spawnPos.position;
            particles[i].Play();
        }
        GameObject weap=Instantiate(Weapon,swordData.spawnPos.position,transform.rotation);
        weap.GetComponent<SpriteRenderer>().sprite=swordData.SwordSprite;
        weap.transform.DOMove(Boss.position,0.5f).OnComplete(()=>{
            EventManager.Broadcast(GameEvent.OnHitBoss);
            Destroy(weap);
        });
    }

   
}
