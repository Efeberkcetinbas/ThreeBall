using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Weapons : MonoBehaviour
{
    public int price; // Price of the Weapon
    
    public bool isPurchased = false; // Purchase status

    public Image weaponImage; // Image of the Weapon
    public GameObject lockImage; // Lock image UI element
    public GameObject coinImage; // Lock image UI element
    public Button button; // Button to interact with the Weapon
    public TextMeshProUGUI priceText; // Text to show the price

    private void Start()
    {
        button = GetComponent<Button>();
        priceText.text = price.ToString();
        //UpdateUI();
    }

    // Update UI elements based on purchase status
    public void UpdateWeaponUI()
    {
        lockImage.SetActive(!isPurchased); // Show/hide lock image
        coinImage.SetActive(!isPurchased);
        button.interactable = !isPurchased; // Enable/disable button based on purchase status
        priceText.gameObject.SetActive(!isPurchased); // Hide price text if purchased
    }
}
