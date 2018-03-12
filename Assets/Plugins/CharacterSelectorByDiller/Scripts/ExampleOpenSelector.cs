using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExampleOpenSelector : MonoBehaviour {

	//The character selector object
	public GameObject m_CharacterSelector;
	//The selected character object
	public GameObject m_SelectedCharacter;

	//The controller for the character selection
	public CharacterSelectionController m_SelectionController;

	//The text showing wheter to open/close the character selection
	public Text m_ToggleText;
	//The button for controlling the character selection opening/closing
	public GameObject m_SelectButton;

	//Whether or not to show the selector
	private bool showSelector = true;

	//For controlling the saved index of the selectable character
	private int savedIndex;
	//The name for the saved index in the player prefs
	private string savedIndexPlayerPrefs = "SelectedIndex";

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start() {
		this.ToggleView();

		savedIndex = PlayerPrefs.GetInt(savedIndexPlayerPrefs, savedIndex);

		//m_SelectedCharacter = m_SelectionController.GetObjectAtIndex(savedIndex);
		m_SelectedCharacter.GetComponent<MeshFilter>().mesh = m_SelectionController.GetObjectAtIndex(savedIndex).GetComponent<MeshFilter>().mesh;
		m_SelectedCharacter.GetComponent<MeshRenderer>().material = m_SelectionController.GetObjectAtIndex(savedIndex).GetComponent<MeshRenderer>().material;

	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update() {
		if(!showSelector) {
			m_SelectedCharacter.transform.Rotate (Vector3.up, 50.0f * Time.deltaTime);
		}
	}

	/// <summary>
	/// Toggles the view.
	/// </summary>
	public void ToggleView() {
		if(showSelector) {
			showSelector = false;
			m_CharacterSelector.SetActive(false);
			m_ToggleText.GetComponent<Text>().text = "Open";
			m_SelectButton.SetActive(false);
		}
		else {
			showSelector = true;
			m_CharacterSelector.SetActive(true);
			m_ToggleText.GetComponent<Text>().text = "Close";
			m_SelectButton.SetActive(true);
			m_SelectionController.PlaceCharacters();
		}
	}

	/// <summary>
	/// Gets the selected character.
	/// </summary>
	public void GetSelectedCharacter() {
		if(m_SelectedCharacter) {
			m_SelectedCharacter.GetComponent<MeshFilter>().mesh = m_SelectionController.GetSelectedObject().GetComponent<MeshFilter>().mesh;
			m_SelectedCharacter.GetComponent<MeshRenderer>().material = m_SelectionController.GetSelectedObject().GetComponent<MeshRenderer>().material;
		}
		else {
			//No character selected
			m_SelectedCharacter.GetComponent<MeshFilter>().mesh = null;
			m_SelectedCharacter.GetComponent<MeshRenderer>().material = null;
		}
	}
}
