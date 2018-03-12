using UnityEngine;
using System.Collections;

public class CharacterSelectionController : MonoBehaviour
{
	//The camera used for the character display
	public GameObject m_CharacterViewCamera;
	public float m_CameraDistance;

	//The array of characters
	public GameObject[] m_Characters;
	
	//The rotation speed of the object when it is selected
	public float m_RotateSpeed = 50.0f;
	//The movement speed of the scrolling left and right
	public float m_MovementSpeed = 2.0f;
	
	//Spacing between each object
	public float m_Spacing = 4.0f;
	//The zoom amount
	public float m_ZoomZ = -4.0f;
	
	//If the object can scroll left or right
	private bool scrollRight = true;
	private bool scrollLeft = true;
	
	//For controlling the movement direction
	private Vector3 moveLeft = new Vector3 (0.0f, 0.0f, 0.0f);
	private Vector3 moveRight = new Vector3 (0.0f, 0.0f, 0.0f);
	private Vector3 moveForward = new Vector3 (0.0f, 0.0f, 0.0f);
	
	//For controlling when to lock player input
	private bool lockMovement = false;
	
	//The currently focused object
	private GameObject selectedObject;
	
	//The max left and right movement
	private float maxLeft;
	private float maxRight;
	
	//For controlling the saved index of the selectable character
	private int savedIndex;
	private string savedIndexPlayerPrefs = "SelectedIndex";
	private int currentIndex;


	//Toggle this depending on if you want to use arrow keys / other keys to move
	private bool useDirectionalKeys = true;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start ()
	{
		//Assign the movement values depending on the chosen numbers
		moveLeft.x = -m_Spacing;
		moveRight.x = m_Spacing;
		moveForward.z = m_ZoomZ;

		//Position the camera in the correct location
		m_CharacterViewCamera.transform.position = new Vector3 (0.0f, 0.0f, m_CameraDistance);
		
		//Calculate the max left and right movement to prevent moving to an empty spot
		maxLeft = (m_Characters.Length * -m_Spacing);
		maxRight = (m_Characters.Length * m_Spacing);
		
		savedIndex = PlayerPrefs.GetInt(savedIndexPlayerPrefs, savedIndex);
		
		this.PlaceCharacters();

		currentIndex = System.Array.IndexOf (m_Characters, selectedObject);
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update ()
	{
		//Allow scrolling if we have not reached the max sides
		if (m_Characters[0].transform.position.x > maxLeft && currentIndex != m_Characters.Length - 1) {
			scrollRight = true;
		} else if (m_Characters [0].transform.position.x == maxLeft) {
			scrollRight = false;
		}
		
		//Allow scrolling if we have not reached the max sides
		if (m_Characters [m_Characters.Length - 1].transform.position.x < maxRight && currentIndex != 0) {
			scrollLeft = true;
		} else if (m_Characters [m_Characters.Length - 1].transform.position.x == maxRight) {
			scrollLeft = false;
		}

		if (useDirectionalKeys) {
			//If scrolling to the left is allowed, check for input
			if (scrollLeft) {
				if (Input.GetKeyDown (KeyCode.LeftArrow) && !lockMovement) {
					this.ScrollLeft();
				}
			}
		
			//If scrolling to the right is allowed, check for input
			if (scrollRight) {
				if (Input.GetKeyDown (KeyCode.RightArrow) && !lockMovement) {
					this.ScrollRight();
				}
			}
		}
		//If there is a selected objected, rotate it
		if (selectedObject) {
			selectedObject.transform.Rotate (Vector3.up, m_RotateSpeed * Time.deltaTime);
		}

		currentIndex = System.Array.IndexOf (m_Characters, selectedObject);
	}

	public void ScrollLeft() {
		if(scrollLeft && !lockMovement && currentIndex != 0) {
			
			//Lock the movement until transition is done
			lockMovement = true;			
			
			StartCoroutine(StartMoveRight());
		}
	}
	
	public void ScrollRight() {
		if(scrollRight && !lockMovement && currentIndex != m_Characters.Length - 1) {
			
			//Lock the movement until transition is done
			lockMovement = true;
			
			StartCoroutine(StartMoveLeft());
		}
	}

	/// <summary>
	/// Gets the object at an index.
	/// </summary>
	/// <returns>The object at index.</returns>
	/// <param name="index">Index.</param>
	public GameObject GetObjectAtIndex(int index) {
		return m_Characters[index];
	}

	/// <summary>
	/// Gets the selected object.
	/// </summary>
	/// <returns>The selected object.</returns>
	public GameObject GetSelectedObject ()
	{
		return selectedObject;
	}
	
	public int GetSelectedObjectIndex ()
	{
		return currentIndex;
	}

	/// <summary>
	/// Saves the selection.
	/// </summary>
	public void SaveSelection ()
	{
		savedIndex = this.GetSelectedObjectIndex();
		PlayerPrefs.SetInt (savedIndexPlayerPrefs, savedIndex);
	}
	
	/// <summary>
	/// Places the characters.
	/// </summary>
	public void PlaceCharacters() {
		float startSpawnLocation;
		
		if(savedIndex == m_Characters.Length) {
			startSpawnLocation = maxLeft;
		}
		else if(savedIndex == 0) {
			startSpawnLocation = 0;
		}
		else {
			startSpawnLocation = (maxLeft + (((m_Characters.Length) - savedIndex) * m_Spacing));
		}
		
		//Spawn up to the saved index
		for(int i = 0; i < m_Characters.Length; i++) {
			m_Characters[i].transform.position = new Vector3(startSpawnLocation, 0.0f, 0.0f);

			//If the spawned object is at the 0 location on X, it is the current selected object
			if(m_Characters[i].transform.position.x == 0.0f) {
				selectedObject = m_Characters[i];
				selectedObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, m_ZoomZ);
			}
			startSpawnLocation += m_Spacing;			
		}
	}
	
	/// <summary>
	/// Starts the move right.
	/// </summary>
	/// <returns>The move right.</returns>
	private IEnumerator StartMoveRight ()
	{
		foreach (GameObject o in m_Characters) {
			StartCoroutine (MoveCharacterRight (o));
		}
		
		yield return null;
	}
	
	/// <summary>
	/// Starts the move left.
	/// </summary>
	/// <returns>The move left.</returns>
	private IEnumerator StartMoveLeft ()
	{
		foreach (GameObject o in m_Characters) {
			StartCoroutine (MoveCharacterLeft (o));
		}
		
		yield return null;
	}
	
	/// <summary>
	/// Moves the character right.
	/// </summary>
	/// <returns>The character right.</returns>
	/// <param name="go">Go.</param>
	private IEnumerator MoveCharacterRight (GameObject go)
	{
		//For controlling our timer
		float t = 0.0f;
		
		//Store the initial position of the object
		Vector3 originalPosition = go.transform.position;
		//Calculate the end position
		Vector3 endPos = go.transform.position + moveRight;
		
		
		while (t < 1.0f) {
			
			//Increment the timer based on the chosen movement speed of scrolling
			t += Time.deltaTime / m_MovementSpeed;
			
			//If the object was previously zoomed, we must un-zoom it
			if (originalPosition.z == m_ZoomZ) {
				go.transform.position = Vector3.Lerp (originalPosition, new Vector3 (originalPosition.x + moveRight.x, originalPosition.y + moveRight.y, 0.0f), t);
			}
			//If the end position will be the focus, we must zoom the object
			else if (endPos.x == 0) {
				go.transform.position = Vector3.Lerp (originalPosition, originalPosition + moveForward + moveRight, t);
				selectedObject = go;
			}
			//Otherwise just move the object
			else {
				go.transform.position = Vector3.Lerp (originalPosition, originalPosition + moveRight, t);
				
			}
			yield return null;
		}
		//Release the lock
		lockMovement = false;
	}
	
	/// <summary>
	/// Moves the character left.
	/// </summary>
	/// <returns>The character left.</returns>
	/// <param name="go">Go.</param>
	private IEnumerator MoveCharacterLeft (GameObject go)
	{
		//For controlling our timer
		float t = 0.0f;
		
		//Store the initial position of the object
		Vector3 originalPosition = go.transform.position;
		//Calculate the end position
		Vector3 endPos = go.transform.position + moveLeft;
		
		while (t < 1.0f) {
			
			//Increment the timer based on the chosen movement speed of scrolling
			t += Time.deltaTime / m_MovementSpeed;
			
			//If the object was previously zoomed, we must un-zoom it
			if (originalPosition.z == m_ZoomZ) {
				go.transform.position = Vector3.Lerp (originalPosition, new Vector3 (originalPosition.x + moveLeft.x, originalPosition.y + moveLeft.y, 0.0f), t);
			}
			//If the end position will be the focus, we must zoom the object
			else if (endPos.x == 0) {
				go.transform.position = Vector3.Lerp (originalPosition, originalPosition + moveForward + moveLeft, t);
				selectedObject = go;
			}
			//Otherwise just move the object
			else {
				go.transform.position = Vector3.Lerp (originalPosition, originalPosition + moveLeft, t);
				
			}
			yield return null;
		}
		//Release the lock
		lockMovement = false;
	}
	
}
