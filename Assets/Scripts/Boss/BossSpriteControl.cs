using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpriteControl : MonoBehaviour
{

    public BossData bossData;

    [SerializeField] private List<Sprite> sprites=new List<Sprite>();

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private int index;
    

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void Start() 
    {
        if(bossData.bossIndex>sprites.Count-1)
        {
            bossData.bossIndex=0;
        }
        spriteRenderer.sprite=sprites[bossData.bossIndex];
    }

    private void OnNextLevel()
    {
        if(bossData.bossIndex>sprites.Count-1)
        {
            bossData.bossIndex=0;
        }
        else
        {
            bossData.bossIndex++;
        }
        
        spriteRenderer.sprite=sprites[bossData.bossIndex];
    }

}
