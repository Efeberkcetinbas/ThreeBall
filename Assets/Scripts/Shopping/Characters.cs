using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Characters : MonoBehaviour
{
    public int price; // Price of the character
    
    public bool isPurchased = false; // Purchase status

    public Image characterImage; // Image of the character
    public GameObject lockImage; // Lock image UI element
    public GameObject coinImage; // Lock image UI element
    public Button button; // Button to interact with the character
    public TextMeshProUGUI priceText; // Text to show the price

    private void Start()
    {
        button = GetComponent<Button>();
        priceText.text = price.ToString();
        //UpdateUI();
    }

    // Update UI elements based on purchase status
    public void UpdateUI()
    {
        lockImage.SetActive(!isPurchased); // Show/hide lock image
        coinImage.SetActive(!isPurchased);
        button.interactable = !isPurchased; // Enable/disable button based on purchase status
        priceText.gameObject.SetActive(!isPurchased); // Hide price text if purchased
    }
}
