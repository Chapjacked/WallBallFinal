using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWallScript : MonoBehaviour {

    //Other scripts
    public SpawnBallScript SpawnBallScript;
    public BallBehaviorScript BallBehaviorScript;
    public CatchBallScript CatchBallScript;
    public ScoringScript ScoringScript;

    //Wall object
    public GameObject FullWall;

    //Speed that you want to rotate the wall
    //public float fRotationSpeed;

    //Variable to set the rotation of the wall
    public float fRotationAngle;

    //Boolean to control when to start/end the wall rotating
    public bool isWallRotating;

    // Use this for initialization
    void Start () {

    }

    public IEnumerator RotateWall(float fRotationSpeed)
    {
        Debug.Log("Rotating Wall...");
        while (isWallRotating == true)
        {
            FullWall.transform.Rotate(0, 0, fRotationSpeed);
            yield return true;
        }
    }
	
	// Update is called once per frame
	void Update () {

        

    }
}
