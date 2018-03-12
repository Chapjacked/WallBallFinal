using UnityEngine;
using System;

[Serializable]
public class ExampleShopItem  {
	public GameObject m_CharacterObject;
	
	//Is the item locked or not? 0 = locked, 1 = unlocked
	public int lockState = 0;
	
	//How many coins we need to unlock this item
	public int costToUnlock = 10000;

	//The cost in real monies to unlock
	public float moneyCostToUnlock = 1.0f;
	
	//The player prefs record for this item
	public string playerPrefsName = "";

	//The name
	public string m_Name;

	//Initialize a color value to store the initial color of on the material
	private Color initialColor = Color.white;

	/// <summary>
	/// Checks the state of the lock.
	/// </summary>
	public void CheckLockState() {
		lockState = PlayerPrefs.GetInt(playerPrefsName);
	}

	/// <summary>
	/// Enables the initial color.
	/// </summary>
	public void EnableInitialColor() {
		m_CharacterObject.GetComponent<Renderer>().material.color = initialColor;
	}

	/// <summary>
	/// Gets the initial color.
	/// </summary>
	/// <returns>The initial color.</returns>
	public Color GetInitialColor() {
		return initialColor;
	}

	/// <summary>
	/// Gets the name of the player prefs.
	/// </summary>
	/// <returns>The player prefs name.</returns>
	public string GetPlayerPrefsName() {
		return playerPrefsName;
	}
}
