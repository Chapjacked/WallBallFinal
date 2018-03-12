using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExampleShopController : MonoBehaviour {

	//How much money the player has
	public int m_MoneyLeft = 4200;
	
	//The text to display the amount of money the player has
	public Transform m_MoneyText;
	
	//The text to display the price of the character
	public Transform m_CoinPriceText;
	//The text to display the money price of the character
	public Transform m_MoneyPriceText;

	//The name of the character
	public Transform m_NameText;

	//An array of the shop items that can be bought/unlocked
	public ExampleShopItem[] m_ShopItems;
	
	//The character selector view
	public Transform m_CharacterSelector;
	
	//The player prefs name
	private string playerPrefsName = "Player";
	//The money name
	private string moneyPlayerPrefsName = "Money";

	//The color that will display on the texture of a shop item when it is not purchased/available
	public Color m_UnavailableColor = Color.black;

	//For keeping track of the indexes
	private GameObject currentSelectedCharacter;
	private int currentSelectedCharacterIndex;
	
	void Start() {

		//For the sake of testing, reset the money value
		this.AddMoney();

		//Get the money value stored in the player prefs
		m_MoneyLeft = PlayerPrefs.GetInt(moneyPlayerPrefsName, m_MoneyLeft);

		//Update the text for the money
		m_MoneyText.GetComponent<Text>().text = m_MoneyLeft.ToString() + " COINS";

		//Check the lock state for each shop item, and update it
		for(int i = 0; i < m_ShopItems.Length; i++) {
			m_ShopItems[i].CheckLockState();
		}
	}
	
	void Update() {
		
		//TEMP for updating the info
		this.UpdateSelectedItemInfo();
		this.UpdateItems();
	}
	
	void OnEnable() {
		m_CharacterSelector.gameObject.SetActive(true);
	}
	
	void OnDisable() {
		if(m_CharacterSelector != null) {
			m_CharacterSelector.gameObject.SetActive(false);
		}
	}
	
	public void UpdateItems() {
		for(int i = 0; i < m_ShopItems.Length; i++) {
			//If the item is unlocked
			if(m_ShopItems[i].lockState > 0) {
				m_ShopItems[i].EnableInitialColor();
			}
			//Else if it is not unlocked
			else if(m_ShopItems[i].lockState < 1) {
				m_ShopItems[i].m_CharacterObject.GetComponent<Renderer>().material.color = m_UnavailableColor;
			}
		}
	}

	/// <summary>
	/// Updates the selected item info.
	/// </summary>
	public void UpdateSelectedItemInfo() {
		currentSelectedCharacterIndex = m_CharacterSelector.GetComponent<CharacterSelectionController>().GetSelectedObjectIndex();

		//Check to make sure the index is not out of array
		if(currentSelectedCharacterIndex > m_ShopItems.Length) {
			currentSelectedCharacterIndex = m_ShopItems.Length;
		}

		//If the item has not been purchased, show the prices
		if(m_ShopItems[currentSelectedCharacterIndex].lockState < 1) {
			m_CoinPriceText.GetComponent<Text>().text = m_ShopItems[currentSelectedCharacterIndex].costToUnlock + " COINS";
			m_MoneyPriceText.GetComponent<Text>().text = "$" + m_ShopItems[currentSelectedCharacterIndex].moneyCostToUnlock + " REAL MONEY";
			m_NameText.GetComponent<Text>().text = m_ShopItems[currentSelectedCharacterIndex].m_Name;
		}
		//Otherwise show that is is already purchased
		else if(m_ShopItems[currentSelectedCharacterIndex].lockState > 0) {
			m_CoinPriceText.GetComponent<Text>().text = "Already Purchased!";
			m_MoneyPriceText.GetComponent<Text>().text = "";
			m_NameText.GetComponent<Text>().text = m_ShopItems[currentSelectedCharacterIndex].m_Name;
		}
	}

	/// <summary>
	/// Buy the item.
	/// </summary>
	public void BuyItem() {
		//If the character is already purchased/unlocked, select it
		if(m_ShopItems[currentSelectedCharacterIndex].lockState > 0) {
			this.SelectCharacter();
			m_CharacterSelector.GetComponent<CharacterSelectionController>().SaveSelection();
		}
		//If the character has not been unlocked/purchased, check to see if the player has enough to purchase it through coins
		else if(m_ShopItems[currentSelectedCharacterIndex].lockState < 1 && 
		        m_ShopItems[currentSelectedCharacterIndex].costToUnlock <= m_MoneyLeft) {

			//Set the lock state to be now purchased/available
			m_ShopItems[currentSelectedCharacterIndex].lockState = 1;

			//Set the lock state to purchased
			PlayerPrefs.SetInt(m_ShopItems[currentSelectedCharacterIndex].GetPlayerPrefsName(), m_ShopItems[currentSelectedCharacterIndex].lockState);

			//Subtract the in game money from their total
			m_MoneyLeft -= m_ShopItems[currentSelectedCharacterIndex].costToUnlock;

			//Update the total money view
			this.UpdateTotalMoney();

			//Set the player prefs to store the new money value
			PlayerPrefs.SetInt(moneyPlayerPrefsName, m_MoneyLeft);

			//Select the character
			this.SelectCharacter();

			//Save the selection
			m_CharacterSelector.GetComponent<CharacterSelectionController>().SaveSelection();

			//Set the color to the correct value
			m_ShopItems[currentSelectedCharacterIndex].EnableInitialColor();
		}

		//Update all the items in the list
		this.UpdateItems();
	}

	/// <summary>
	/// Updates the total money display value.
	/// </summary>
	private void UpdateTotalMoney() {
		m_MoneyText.GetComponent<Text>().text = m_MoneyLeft.ToString() + " COINS";
	}

	/// <summary>
	/// Selects the character.
	/// </summary>
	private void SelectCharacter() {
		PlayerPrefs.SetInt(playerPrefsName, currentSelectedCharacterIndex);
	}

	private void AddMoney() {
		PlayerPrefs.SetInt(moneyPlayerPrefsName, 4200);
	}

	/// <summary>
	/// Resets the purchases.
	/// </summary>
	public void ResetPurchases() {
		for(int i = 0; i < m_ShopItems.Length; i++) {
			m_ShopItems[i].lockState = 0;
			PlayerPrefs.SetInt(m_ShopItems[i].GetPlayerPrefsName(), m_ShopItems[i].lockState);
		}

		m_MoneyLeft = 4200;
		this.AddMoney();
		this.UpdateTotalMoney();

		PlayerPrefs.SetInt(moneyPlayerPrefsName, 4200);
	}
}
