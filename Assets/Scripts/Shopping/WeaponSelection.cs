using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    public List<Weapons> weapons=new List<Weapons>();

    public SwordData swordData;

    public Color color;
    
    


    public void SelectWeapon(int selectedIndex)
    {
        if(weapons[selectedIndex-1].button.interactable)
        {
            swordData.SwordSprite=weapons[selectedIndex-1].weaponImage.sprite;
            EventManager.Broadcast(GameEvent.OnSelection);
            EventManager.Broadcast(GameEvent.OnCharacterSelected);
            weapons[selectedIndex-1].lockImage.SetActive(false);

            if(weapons[selectedIndex-1].isPurchased)
                ScoreManager.Instance.UpdateScore(-weapons[selectedIndex-1].price);

            weapons[selectedIndex-1].weaponData.isPurchased=true;

            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].button.image.color=Color.white;
            }

            weapons[selectedIndex-1].button.image.color=color;
        }
    }
}
