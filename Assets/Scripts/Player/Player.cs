using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;

    private int randomIndex;
    public ParticleSystem particle;
    public ParticleSystem leaveParticle;


    private SpriteRenderer spriteRenderer;

    public PlayerData playerData;
    private void Start() 
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        spriteRenderer.sprite=playerData.sprite;
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnSaw,OnSaw);
        EventManager.AddHandler(GameEvent.OnCharacterSelected,OnCharacterSelected);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnSaw,OnSaw);
        EventManager.RemoveHandler(GameEvent.OnCharacterSelected,OnCharacterSelected);
    }

    private void OnCharacterSelected()
    {
        spriteRenderer.sprite=playerData.sprite;
    }

   

   

    private void OnSaw()
    {
        spriteRenderer.color=Color.red;
        StartCoroutine(GetBackColor());
    }

    private IEnumerator GetBackColor()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color=Color.white;
    }
}
