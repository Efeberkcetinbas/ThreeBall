using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;

    private int randomIndex;
    public ParticleSystem particle;
    public ParticleSystem leaveParticle;

    [SerializeField] private List<Sprite> emojis=new List<Sprite>();
    private SpriteRenderer spriteRenderer;

    public PlayerData playerData;
    private void Start() 
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        OnNextLevel();
        //spriteRenderer.sprite=playerData.sprite;
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnSaw,OnSaw);
        EventManager.AddHandler(GameEvent.OnCharacterSelected,OnCharacterSelected);
        EventManager.AddHandler(GameEvent.OnUpdateEmoji,OnUpdateEmoji);
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);

    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnSaw,OnSaw);
        EventManager.RemoveHandler(GameEvent.OnCharacterSelected,OnCharacterSelected);
        EventManager.RemoveHandler(GameEvent.OnUpdateEmoji,OnUpdateEmoji);
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }

    private void OnCharacterSelected()
    {
        //spriteRenderer.sprite=playerData.sprite;
    }


    private void OnUpdateEmoji()
    {
        spriteRenderer.sprite=emojis[playerData.characterIndex];
    }

    private void OnNextLevel()
    {
        playerData.characterIndex=1;
        spriteRenderer.sprite=emojis[playerData.characterIndex];
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
