using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuGameplayBGScript : MonoBehaviour {

    public SpawnMovingObjectScript SpawnMovingObjectScript;

    //Counter to determine when to start the background stuff
    public int numBackgroundStartCounter;

	// Use this for initialization
	void Start () {

        StartCoroutine(RunCounter());
		
	}

    public IEnumerator RunCounter()
    {

        //WaitForSeconds(2);

        numBackgroundStartCounter += 1;

        yield return true;


    }
	
	// Update is called once per frame
	void Update () {

        //RemoveWallPiece();
		
	}

    //public void RemoveWallPiece()
    //{
    //    //pick a random number between 0 and the list of wall pieces
    //    int WallPieceToRemove = Random.Range(0, 28);

    //    if (WallPieces[WallPieceToRemove] != null)
    //    {
    //        if (numBackgroundStartCounter == 5 || 7 || 9 || 11 || 13)
    //        {
    //            WallPieces[WallPieceToRemove].SetActive(false);

    //            if (SpawnMovingObjectScript.RemoveWallCounter < (SpawnMovingObjectScript.ArrayOfWhenToRemoveWallPieces.Length - 1))
    //            {
    //                SpawnMovingObjectScript.RemoveWallCounter += 1;
    //            }
    //        }
    //        else
    //        {
    //            //Pick a new random wall piece to remove
    //            WallPieceToRemove = Random.Range(0, WallPieces.Length);
    //            //Attempt to remove that wall piece
    //            RemoveWallPiece();

    //        }
    //    }
    //    //WallPieces.(WallPieceToRemove);

    //    //Debug.Log("New length of array: " + WallPieces.Length);
    //}
}
