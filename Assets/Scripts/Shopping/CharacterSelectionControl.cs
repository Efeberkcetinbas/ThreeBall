using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionControl : MonoBehaviour
{
    public List<Characters> characters = new List<Characters>();
    public PlayerData playerData;
    public Color selectedColor;

    public void SelectCharacter(int selectedIndex)
    {
        // Ensure valid index
        if (selectedIndex < 0 || selectedIndex >= characters.Count)
        {
            Debug.LogError("Invalid character index");
            return;
        }

        Characters selectedCharacter = characters[selectedIndex];

        // Check if the character can be selected or purchased
        if (selectedCharacter.button.interactable)
        {
            // Update player data to use the selected character's image and power
            playerData.sprite = selectedCharacter.characterImage.sprite;
            playerData.IncreasePower = selectedCharacter.power;

            // Broadcast relevant events
            EventManager.Broadcast(GameEvent.OnSelection);
            EventManager.Broadcast(GameEvent.OnCharacterSelected);

            // Mark the character as purchased and update visuals
            selectedCharacter.characterData.isPurchased = true;
            selectedCharacter.lockImage.SetActive(false);
            selectedCharacter.button.image.color = selectedColor;

            // Deduct the score if the character hasn't been purchased yet
            if (!selectedCharacter.isPurchased)
            {
                ScoreManager.Instance.UpdateScore(-selectedCharacter.price);
                selectedCharacter.isPurchased = true;
                SaveCharacterPurchase(selectedIndex);  // Save purchase state
            }

            // Reset colors for all other characters
            foreach (var character in characters)
            {
                character.button.image.color = Color.white;
            }
            selectedCharacter.button.image.color = selectedColor;
        }
    }

    private void SaveCharacterPurchase(int characterIndex)
    {
        PlayerPrefs.SetInt($"CharacterPurchased_{characterIndex}", 1);  // Save as purchased (1 means purchased)
        PlayerPrefs.Save();
    }

    public void LoadCharacterPurchases()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            // Check if the character has been purchased before
            if (PlayerPrefs.GetInt($"CharacterPurchased_{i}", 0) == 1)
            {
                characters[i].characterData.isPurchased = true;
                characters[i].lockImage.SetActive(false);
                characters[i].button.interactable = true;
            }
        }
    }

    private void Start()
    {
        LoadCharacterPurchases();  // Load saved purchases when the game starts
    }
}
