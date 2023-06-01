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

    private void OnNextLevel()
    {
        spriteRenderer.sprite=sprites[bossData.bossIndex];
    }

}
