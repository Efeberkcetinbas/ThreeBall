using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> spriteRenderer=new List<SpriteRenderer>();
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private PlayerData playerData;
 

    

    private void Start()
    {
        OnUpdateWeapon();
    }

    private void OnUpdateWeapon()
    {
        for (int i = 0; i < spriteRenderer.Count; i++)
        {
            spriteRenderer[i].sprite=weaponData.sprite;    
        }
        
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnSpawnWeapon,OnSpawnWeapon);
        EventManager.AddHandler(GameEvent.OnUpdateWeapon,OnUpdateWeapon);
        EventManager.AddHandler(GameEvent.OnHitBoss,OnHitBoss);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnSpawnWeapon,OnSpawnWeapon);
        EventManager.RemoveHandler(GameEvent.OnUpdateWeapon,OnUpdateWeapon);
        EventManager.RemoveHandler(GameEvent.OnHitBoss,OnHitBoss);
    }
    private void OnSpawnWeapon()
    {
        for (int i = 0; i < spriteRenderer.Count; i++)
        {
            spriteRenderer[i].transform.localScale=Vector2.one;    
        }
        spriteRenderer[playerData.selectedCharacterIndex].transform.localScale=Vector2.zero;
    }

    private void OnHitBoss()
    {
        
        spriteRenderer[playerData.selectedCharacterIndex].transform.DOScale(Vector2.one,1f);
    }

}
