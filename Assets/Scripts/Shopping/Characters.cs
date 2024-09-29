using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Characters : MonoBehaviour
{
    public int price;
    public int power;

    public bool isPurchased = false;
    public bool canBuy = false;

    public Image characterImage;
    public GameObject lockImage, goldImage, tickImage;

    internal Button button;
    public TextMeshProUGUI priceText;

    public CharacterData characterData;
    public PlayerData playerData;

    private void Start()
    {
        button = GetComponent<Button>();
        priceText.text = price.ToString();
        CheckPurchase();
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnButtonClicked, OnButtonClicked);
        EventManager.AddHandler(GameEvent.OnCharacterSelected, OnCharacterSelected);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnButtonClicked, OnButtonClicked);
        EventManager.RemoveHandler(GameEvent.OnCharacterSelected, OnCharacterSelected);
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
        // If already purchased, update the visuals
        if (characterData.isPurchased)
        {
            lockImage.SetActive(false);
            button.interactable = true;
            goldImage.SetActive(false);
            //tickImage.SetActive(true);
            priceText.gameObject.SetActive(false);
            isPurchased = true;
        }

        // Check if the player can buy the character
        if (ScoreManager.Instance.score >= price || characterData.isPurchased)
        {
            button.interactable = true;
            canBuy = true;
        }
        else
        {
            button.interactable = false;
            canBuy = false;
        }
    }
}
