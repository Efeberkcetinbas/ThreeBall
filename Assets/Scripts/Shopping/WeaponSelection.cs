using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    public List<Weapons> weapons=new List<Weapons>();

    public SwordData swordData;
    
    


    public void SelectWeapon(int selectedIndex)
    {
        if(weapons[selectedIndex-1].button.interactable)
        {
            swordData.SwordSprite=weapons[selectedIndex-1].weaponImage.sprite;
            EventManager.Broadcast(GameEvent.OnSelection);
            EventManager.Broadcast(GameEvent.OnCharacterSelected);
            weapons[selectedIndex-1].lockImage.SetActive(false);
            ScoreManager.Instance.UpdateScore(-weapons[selectedIndex-1].price);
            weapons[selectedIndex-1].weaponData.isPurchased=true;
        }
    }
}
