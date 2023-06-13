using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Characters : MonoBehaviour
{
    public int price;

    public bool isPurchased=false;
    public bool canBuy=false;

    public Image characterImage;
    public GameObject lockImage;

    internal Button button;

    public TextMeshProUGUI priceText;

    public CharacterData characterData;

    private void Start() 
    {
        button=GetComponent<Button>();
        priceText.text=price.ToString();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnButtonClicked,OnButtonClicked);
        EventManager.AddHandler(GameEvent.OnCharacterSelected,OnCharacterSelected);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnButtonClicked,OnButtonClicked);
        EventManager.RemoveHandler(GameEvent.OnCharacterSelected,OnCharacterSelected);
    }


    private void OnButtonClicked()
    {
        CheckPurchase();
    }

    private void OnCharacterSelected()
    {
        CheckPurchase();
    }

    private void CheckPurchase()
    {
        if(characterData.isPurchased)
        {
            priceText.text="B";
            lockImage.SetActive(false);
            button.interactable=true;
        }

        if(ScoreManager.Instance.score>=price || isPurchased || characterData.isPurchased)
        {
            button.interactable=true;
            canBuy=true;
        }

        else
        {
            button.interactable=false;
            canBuy=false;
        }
    }
}
