using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private RectTransform StartPanel,CharacterPanel,WeaponPanel;

    [SerializeField] private float StartX,StartY,CharacterX,CharacterY,WeaponX,WeaponY,duration;

    [SerializeField] private SoundManager soundManager;

    public bool oneTime=true;

    private void Start() 
    {
        soundManager=SoundManager.Instance;
    }

    private void Update() 
    {
        if(oneTime)
        {
            if(Input.touchCount>=1)
            {
                GameManager.Instance.isGameEnd=false;
                StartPanel.gameObject.SetActive(false);
                oneTime=false;
            }
        }
    }


    public void OpenCharacterPanel()
    {
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        CharacterPanel.gameObject.SetActive(true);
        CharacterPanel.DOAnchorPos(Vector2.zero,duration);
        soundManager.Play("CharacterMenu");
    }

    public void OpenWeaponPanel()
    {
        StartPanel.DOAnchorPos(new Vector2(StartX,StartY),duration).OnComplete(()=>StartPanel.gameObject.SetActive(false));
        WeaponPanel.gameObject.SetActive(true);
        WeaponPanel.DOAnchorPos(Vector2.zero,duration);
        soundManager.Play("WeaponMenu");
    }

    public void BackToStart(bool isOnCharacter)
    {
        soundManager.Play("StartMenu");

        if(isOnCharacter)
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration);
            CharacterPanel.DOAnchorPos(new Vector2(CharacterX,CharacterY),duration).OnComplete(()=>CharacterPanel.gameObject.SetActive(false));
        }
        else
        {
            StartPanel.gameObject.SetActive(true);
            StartPanel.DOAnchorPos(Vector2.zero,duration);
            WeaponPanel.DOAnchorPos(new Vector2(WeaponX,WeaponY),duration).OnComplete(()=>WeaponPanel.gameObject.SetActive(false));
        }

    }

    
}
