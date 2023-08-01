using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Weapons : MonoBehaviour
{
    //Character ile ayni neredeyse daha sonra kodu gelistir
    public int price;

    public bool isPurchased=false;
    public bool canBuy=false;

    public Image weaponImage;
    public GameObject lockImage,goldImage,TickImage;

    internal Button button;

    public TextMeshProUGUI priceText;

    public WeaponData weaponData;
    private void Start() 
    {
        button=GetComponent<Button>();
        priceText.text=price.ToString();
    }

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnButtonClicked,OnButtonClicked);
        EventManager.AddHandler(GameEvent.OnWeaponSelected,OnWeaponSelected);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnButtonClicked,OnButtonClicked);
        EventManager.RemoveHandler(GameEvent.OnWeaponSelected,OnWeaponSelected);
    }


    private void OnButtonClicked()
    {
        CheckPurchase();
    }

    private void OnWeaponSelected()
    {
        CheckPurchase();
    }

    private void CheckPurchase()
    {
        if(weaponData.isPurchased)
        {
            priceText.text="B";
            priceText.gameObject.SetActive(false);
            goldImage.SetActive(false);
            TickImage.SetActive(true);
            lockImage.SetActive(false);
            button.interactable=true;
            
            isPurchased=true;
        }

        if(ScoreManager.Instance.score>=price  || weaponData.isPurchased)
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
