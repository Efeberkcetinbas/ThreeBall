using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpriteControl : MonoBehaviour
{
    private LevelManager levelManager;

    private int index;

    [SerializeField] private List<Sprite> sprites=new List<Sprite>();

    [SerializeField] private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        levelManager=LevelManager.Instance;
        OnNextLevel();
    }

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
        spriteRenderer.sprite=sprites[levelManager.levelIndex];
    }

}
