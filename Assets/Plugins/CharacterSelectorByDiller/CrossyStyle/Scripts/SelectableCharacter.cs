using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Selectable character.
/// </summary>
public class SelectableCharacter : MonoBehaviour {

    //Other scripts
    //public PlayerCoinsScript PlayerCoinsScript;


	//Name given, and what will be displayed
	public string m_CharacterName;

	//For the locked and unlocked coloring of the sprite
	private Color unlockedColor = Color.white;
	private Color lockedColor = Color.black;

	//Storing info needed for controlling the lock states
	private string unlockSaveState;
	public int isUnlocked = 0;

	//Image of the character
	public Image characterImage;

    //Text for the "Select Button"
    public Text SelectButtonText;

    //Component to use to get the text of the "Select Button"
    public GameObject SelectButtonGameObject;

	public void Start() {
		this.LoadLockState();
		this.CheckLockedState();

        this.isUnlocked = 0;

        //SelectButtonText = SelectButtonGameObject.GetComponent<Text>();
	}

	/// <summary>
	/// Checks the state of the locked.
	/// </summary>
	private void CheckLockedState() {
		if(isUnlocked == 0) {
			characterImage.color = lockedColor;
		}
		else {
			characterImage.color = unlockedColor;
		}
	}

	/// <summary>
	/// Loads the state of the lock.
	/// </summary>
	private void LoadLockState() {
		if(PlayerPrefs.HasKey(unlockSaveState)) {
			isUnlocked = PlayerPrefs.GetInt(unlockSaveState);
		}
	}

	/// <summary>
	/// Saves the state of the lock.
	/// </summary>
	private void SaveLockState() {
		PlayerPrefs.SetInt(unlockSaveState, isUnlocked);
		PlayerPrefs.Save();
	}

	/// <summary>
	/// Buies the character.
	/// </summary>
	//public void BuyCharacter() {
 //       //Check to see if the player has enough coins
 //       if (PlayerCoinsScript.numPlayerCoins >= 100 && this.isUnlocked == 0)
 //       {
 //           isUnlocked = 1;
 //           this.SaveLockState();
 //           this.CheckLockedState();
 //           PlayerCoinsScript.numPlayerCoins = -100;
 //           SelectButtonText.text = "Buy (100 coins)";
 //       }
 //       else if (this.isUnlocked == 1)
 //       {
 //           SelectButtonText.text = "Select";
 //           Debug.Log("Skin already purchased");
 //       }
 //       else
 //       {
 //           SelectButtonText.text = "Need Coins";
 //           Debug.Log("Could not purchase - not enough coins");
 //       }
	//}

	/// <summary>
	/// Loads the character.
	/// </summary>
	public void LoadCharacter() {
		characterImage = this.GetComponent<Image>();
		unlockSaveState = m_CharacterName + "_unlock";

		this.LoadLockState();
		this.CheckLockedState();
	}
}
