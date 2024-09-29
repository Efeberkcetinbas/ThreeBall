using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<Characters> characters = new List<Characters>(); // List of characters
    public List<Weapons> weapons = new List<Weapons>(); // List of weapons
    public PlayerData playerData; // Reference to player data
    public WeaponData weaponData;
    
    private int selectedCharacterIndex = -1; // Track the currently selected character index
    private int selectedWeaponIndex = -1; // Track the currently selected weapon index


    // Method to purchase a character
    public void PurchaseCharacter(int characterIndex)
    {
        if (characterIndex < 0 || characterIndex >= characters.Count)
        {
            Debug.LogError("Invalid character index.");
            return;
        }

        Characters selectedCharacter = characters[characterIndex];

        if (!selectedCharacter.isPurchased && ScoreManager.Instance.score >= selectedCharacter.price)
        {
            ScoreManager.Instance.score -= selectedCharacter.price; // Deduct price
            PlayerPrefs.SetInt("Score",ScoreManager.Instance.score);
            UIManager.Instance.UpgradeScoreText();
            selectedCharacter.isPurchased = true; // Mark as purchased
            selectedCharacter.UpdateUI(); // Update UI
            SaveCharacterPurchase(characterIndex); // Save purchase status
            Debug.Log($"Purchased {selectedCharacter.characterImage.name} for {selectedCharacter.price}.");
        }
        else if (selectedCharacter.isPurchased)
        {
            Debug.Log("Character already purchased.");
        }
        else
        {
            Debug.Log("Not enough score to purchase the character.");
        }

        UpdateAllButtons();
    }

    // Method to purchase a weapon
    public void PurchaseWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weapons.Count)
        {
            Debug.LogError("Invalid weapon index.");
            return;
        }

        Weapons selectedWeapon = weapons[weaponIndex];

        if (!selectedWeapon.isPurchased && ScoreManager.Instance.score >= selectedWeapon.price)
        {
            ScoreManager.Instance.score -= selectedWeapon.price; // Deduct price
            PlayerPrefs.SetInt("Score",ScoreManager.Instance.score);
            UIManager.Instance.UpgradeScoreText();
            selectedWeapon.isPurchased = true; // Mark as purchased
            selectedWeapon.UpdateWeaponUI(); // Update UI
            SaveWeaponPurchase(weaponIndex); // Save purchase status
            Debug.Log($"Purchased {selectedWeapon.weaponImage.name} for {selectedWeapon.price}.");
        }
        else if (selectedWeapon.isPurchased)
        {
            Debug.Log("Weapon already purchased.");
        }
        else
        {
            Debug.Log("Not enough score to purchase the weapon.");
        }

        UpdateAllButtons();
    }

    // Method to select a character
    public void SelectCharacter(int characterIndex)
    {
        if (characterIndex < 0 || characterIndex >= characters.Count)
        {
            Debug.LogError("Invalid character index.");
            return;
        }

        Characters selectedCharacter = characters[characterIndex];

        if (selectedCharacter.isPurchased)
        {
            selectedCharacterIndex = characterIndex; // Update selected index
            playerData.sprite = selectedCharacter.characterImage.sprite; // Update player data sprite
            EventManager.Broadcast(GameEvent.OnCharacterSelected);
            

            foreach (var character in characters)
            {
                character.button.image.color = Color.white; // Reset button color
            }
            selectedCharacter.button.image.color = Color.green; // Highlight selected character
            Debug.Log($"Selected character: {selectedCharacter.characterImage.name}");
        }
        else
        {
            Debug.Log("Character not purchased yet.");
        }
    }

    // Method to select a weapon
    public void SelectWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weapons.Count)
        {
            Debug.LogError("Invalid weapon index.");
            return;
        }

        Weapons selectedWeapon = weapons[weaponIndex];

        if (selectedWeapon.isPurchased)
        {
            selectedWeaponIndex = weaponIndex; // Update selected weapon index
            weaponData.sprite = selectedWeapon.weaponImage.sprite; // Update player data with weapon sprite
            EventManager.Broadcast(GameEvent.OnUpdateWeapon);
            foreach (var weapon in weapons)
            {
                weapon.button.image.color = Color.white; // Reset button color
            }
            selectedWeapon.button.image.color = Color.green; // Highlight selected weapon
            Debug.Log($"Selected weapon: {selectedWeapon.weaponImage.name}");
        }
        else
        {
            Debug.Log("Weapon not purchased yet.");
        }
    }

    // Save the purchase status to PlayerPrefs
    private void SaveCharacterPurchase(int characterIndex)
    {
        PlayerPrefs.SetInt($"CharacterPurchased_{characterIndex}", 1);
        PlayerPrefs.Save();
    }

    private void SaveWeaponPurchase(int weaponIndex)
    {
        PlayerPrefs.SetInt($"WeaponPurchased_{weaponIndex}", 1);
        PlayerPrefs.Save();
    }

    // Load character and weapon purchase statuses
    public void LoadPurchases()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (PlayerPrefs.GetInt($"CharacterPurchased_{i}", 0) == 1)
            {
                characters[i].isPurchased = true;
                characters[i].UpdateUI();
            }
        }

        for (int i = 0; i < weapons.Count; i++)
        {
            if (PlayerPrefs.GetInt($"WeaponPurchased_{i}", 0) == 1)
            {
                weapons[i].isPurchased = true;
                weapons[i].UpdateWeaponUI();
            }
        }

        UpdateAllButtons();
    }

    // Update interactable state of all buttons for characters and weapons
    private void UpdateAllButtons()
    {
        foreach (var character in characters)
        {
            if (character.isPurchased)
            {
                character.button.interactable = true;
            }
            else
            {
                character.button.interactable = ScoreManager.Instance.score >= character.price;
            }
        }

        foreach (var weapon in weapons)
        {
            if (weapon.isPurchased)
            {
                weapon.button.interactable = true;
            }
            else
            {
                weapon.button.interactable = ScoreManager.Instance.score >= weapon.price;
            }
        }
    }

    private void Start()
    {
        LoadPurchases();
    }

    public void OnCharacterButtonClicked(int characterIndex)
    {
        PurchaseCharacter(characterIndex);
        SelectCharacter(characterIndex);
    }

     public void OnWeaponButtonClicked(int weaponIndex)
    {
        PurchaseWeapon(weaponIndex);
        SelectWeapon(weaponIndex);
    }
}
