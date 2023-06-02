using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private RectTransform StartPanel,CharacterPanel,WeaponPanel;


    [SerializeField] private Image Fade;

    [SerializeField] private float StartX,StartY,CharacterX,CharacterY,WeaponX,WeaponY,duration;

    

    private bool oneTime=true;

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnNextLevel,OnNextLevel);
    }


    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnNextLevel,OnNextLevel);
    }
    
    private void Update() 
    {
        if(oneTime)
        {
            if(Input.touchCount>=1 && Input.GetTouch(0).position.y>Screen.height/2.5f)
            {
                GameManager.Instance.isGameEnd=false;
                StartPanel.gameObject.SetActive(false);
                oneTime=false;
            }
        }
    }

    private void OnNextLevel()
    {
        StartPanel.gameObject.SetActive(true);
        StartPanel.DOAnchorPos(Vector2.zero,0.1f);
        StartCoroutine(Blink(Fade.gameObject,Fade));
    }


  

    private IEnumerator Blink(GameObject gameObject,Image image)
    {
        
        gameObject.SetActive(true);
        image.color=new Color(0,0,0,1);
        image.DOFade(0,0.2f);
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        oneTime=true;

    }


    public void OpenCharacterPanel()
    {
        oneTime=false;
        EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        CharacterPanel.gameObject.SetActive(true);
        CharacterPanel.DOAnchorPos(Vector2.zero,duration);
    }

    public void OpenWeaponPanel()
    {
        oneTime=false;
        EventManager.Broadcast(GameEvent.OnButtonClicked);
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        WeaponPanel.gameObject.SetActive(true);
        WeaponPanel.DOAnchorPos(Vector2.zero,duration);
    }

    public void BackToStart(bool isOnCharacter)
    {

        if(isOnCharacter)
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration).OnComplete(()=>oneTime=true);
            CharacterPanel.DOAnchorPos(new Vector2(CharacterX,CharacterY),duration).OnComplete(()=>CharacterPanel.gameObject.SetActive(false));
        }
        else
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration).OnComplete(()=>oneTime=true);
            WeaponPanel.DOAnchorPos(new Vector2(WeaponX,WeaponY),duration).OnComplete(()=>WeaponPanel.gameObject.SetActive(false));
        }

        EventManager.Broadcast(GameEvent.OnButtonClicked);

    }

    

    
}
