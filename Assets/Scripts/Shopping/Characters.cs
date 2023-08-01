using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Characters : MonoBehaviour
{
    public int price;
    public int power;

    public bool isPurchased=false;
    public bool canBuy=false;

    public Image characterImage;
    public GameObject lockImage,goldImage,TickImage;

    internal Button button;

    public TextMeshProUGUI priceText;

    public CharacterData characterData;
    public PlayerData playerData;
    public CharacterSelectionControl characterSelectionControl;

    public Color color;

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
            //priceText.text="B";
            
            lockImage.SetActive(false);
            button.interactable=true;
            for (int i = 0; i < characterSelectionControl.characters.Count; i++)
            {
                characterSelectionControl.characters[i].button.image.color=Color.white;
            }

            button.image.color=color;
            playerData.IncreasePower=power;

            //button.image.color=Color.green;
            goldImage.SetActive(false);
            TickImage.SetActive(true);
            priceText.gameObject.SetActive(false);
            isPurchased=true;
        }

        if(ScoreManager.Instance.score>=price || characterData.isPurchased)
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
