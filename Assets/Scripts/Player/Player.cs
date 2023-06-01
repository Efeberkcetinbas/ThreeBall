using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;

    private int randomIndex;
    public ParticleSystem particle;
    public ParticleSystem leaveParticle;

    public List<Sprite> sprites=new List<Sprite>();

    private SpriteRenderer spriteRenderer;
    private void Start() 
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        //MakeRandomSprite(spriteRenderer);
    }

    private void OnEnable() 
    {
        //EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);   
        EventManager.AddHandler(GameEvent.OnSaw,OnSaw);
    }

    private void OnDisable() 
    {
        //EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
        EventManager.RemoveHandler(GameEvent.OnSaw,OnSaw);
    }

    private void OnNextLevel()
    {
        MakeRandomSprite(spriteRenderer);
    }

    private Sprite MakeRandomSprite(SpriteRenderer spriteRenderer)
    {
        randomIndex=Random.Range(0,sprites.Count);
        spriteRenderer.sprite=sprites[randomIndex];
        return spriteRenderer.sprite;
        
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
