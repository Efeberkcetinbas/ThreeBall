using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionControl : MonoBehaviour
{
    public List<Characters> characters=new List<Characters>();

    public PlayerData playerData;

    public Color color;
    


    public void SelectCharacter(int selectedIndex)
    {
        
        if(characters[selectedIndex-1].button.interactable)
        {
            playerData.sprite=characters[selectedIndex-1].characterImage.sprite;
            EventManager.Broadcast(GameEvent.OnSelection);
            EventManager.Broadcast(GameEvent.OnCharacterSelected);
            characters[selectedIndex-1].lockImage.SetActive(false);
            playerData.IncreasePower=characters[selectedIndex-1].power;
            if(!characters[selectedIndex-1].isPurchased)
                ScoreManager.Instance.UpdateScore(-characters[selectedIndex-1].price);
            
            characters[selectedIndex-1].characterData.isPurchased=true;

            for (int i = 0; i < characters.Count; i++)
            {
                characters[i].button.image.color=Color.white;
            }

            characters[selectedIndex-1].button.image.color=color;
        }
    }
}
