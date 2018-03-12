using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Character selector 2 d crossy style.
/// </summary>
public class CharacterSelector_2D_Crossy : MonoBehaviour 
{

    //Other Scripts
    public SelectableCharacter SelectableCharacter;
	//The scrolling panel section
	public RectTransform m_ScrollPanel;
	//List of character objects to be displayed
	public GameObject[] m_Characters;
	//The center location for the images
	public RectTransform m_Center;

	//Display on the UI for the current character
	public Text m_CharacterNameText;

	//For tracking distances of the character images
	private float[] distances;
	private float[] distReposition;

	//Flagging when the screen is being dragged
	private bool isDragging = false;

	//Distance between images
	private int imageDistance;
	//Number of the image index in the center of the screen (currently selected)
	private int selectedCharacter;
	//Length of the characters available
	private int imageLength;

	//List of character objects
	private List<SelectableCharacter> characters = new List<SelectableCharacter>();

	private void Start()
	{
		//Set reference to the lenght of the characters as this will not change dynamically
		imageLength = m_Characters.Length;
		//Set the size of the distance calculations
		distances = new float[imageLength];
		//Set the size of the distance repositions
		distReposition = new float[imageLength];

		//Get the distance between the displayed characters
		imageDistance  = (int)Mathf.Abs(m_Characters[1].GetComponent<RectTransform>().anchoredPosition.x - m_Characters[0].GetComponent<RectTransform>().anchoredPosition.x);
	
		this.PopulateCharacters();
	}

	private void Update()
	{
		//Reposition the layout to show the selected object
		this.Reposition();

		//If the user is not dragging, expand the size of the selected player
		if (!isDragging && m_Characters[selectedCharacter])
		{
			RepositionToCharacter (-m_Characters[selectedCharacter].GetComponent<RectTransform>().anchoredPosition.x);
			this.AdjustSelectedImageSize();
			m_CharacterNameText.text = characters[selectedCharacter].m_CharacterName;

            if (SelectableCharacter.isUnlocked == 1)
            {
                SelectableCharacter.SelectButtonText.text = "Select";
            }
            else if (SelectableCharacter.isUnlocked == 0)
            {
                SelectableCharacter.SelectButtonText.text = "Buy (100)";
            }

        }
	}

	/// <summary>
	/// Reposition this instance.
	/// </summary>
	private void Reposition() {
		//For each of the characters in our array, we want to reposition them so that the focused
		// character is in the x=0 location
		for (int i = 0; i < m_Characters.Length; i++)
		{
			distReposition[i] = m_Center.GetComponent<RectTransform>().position.x - m_Characters[i].GetComponent<RectTransform>().position.x;
			distances[i] = Mathf.Abs(distReposition[i]);
		}

		//Get the minimum distance
		float minDistance = Mathf.Min(distances);

		//Find the selected character
		for (int a = 0; a < m_Characters.Length; a++)
		{
			if (minDistance == distances[a])
			{
				selectedCharacter = a;
			}
		}
	}

	/// <summary>
	/// Repositions to character.
	/// </summary>
	/// <param name="position">Position.</param>
	private void RepositionToCharacter(float position)
	{
		float newX = Mathf.Lerp (m_ScrollPanel.anchoredPosition.x, position, Time.deltaTime * 5f);
		Vector2 newPosition = new Vector2 (newX, m_ScrollPanel.anchoredPosition.y);

		m_ScrollPanel.anchoredPosition = newPosition;
	}

	/// <summary>
	/// Adjusts the size of the selected image.
	/// </summary>
	private void AdjustSelectedImageSize() {

		for(int i = 0; i < imageLength; i++) {
			m_Characters[i].GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
		}

		m_Characters[selectedCharacter].GetComponent<RectTransform>().localScale = new Vector3(1.5f,1.5f,1);
	}

	/// <summary>
	/// Populates the characters.
	/// </summary>
	private void PopulateCharacters() {
		for(int i = 0; i < m_Characters.Length; i++) {
			characters.Add(m_Characters[i].GetComponent<SelectableCharacter>());
			characters[i].LoadCharacter();
		}
	}

	/// <summary>
	/// Starts the drag.
	/// </summary>
	public void StartDrag()
	{
		isDragging = true;
	}

	/// <summary>
	/// Ends the drag.
	/// </summary>
	public void EndDrag()
	{
		isDragging = false;
	}

	/// <summary>
	/// Gets the current character index.
	/// </summary>
	/// <returns>The current character.</returns>
	public int GetCurrentCharacter() {
		return selectedCharacter;
	}

	/// <summary>
	/// Selects the current character.
	/// </summary>
	//public void SelectCurrentCharacter() {
	//	characters[selectedCharacter].BuyCharacter();
	//	m_CharacterNameText.text = characters[selectedCharacter].m_CharacterName;
	//}
}













