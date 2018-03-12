using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMovingObjectScript : MonoBehaviour {

    //Other scripts
    public SpawnBallScript SpawnBallScript;
    public CatchBallScript CatchBallScript;
    public BallBehaviorScript BallBehaviorScript;
    public ScoringScript ScoringScript;

    //Object used to spawn
    public GameObject ObjectToSpawn;

    //Array to hold when the red cubes spawn
    public int[] ArrayOfWhenToSpawnRedCubes;

    public static int SpawnCubeCounter;

    //How much force to apply to the spawned object in the X direction
    public float AmountOfForceToApplyX;

    //How much force to apply to the spawned object in the Y direction
    public float AmountOfForceToApplyY;

    //How much force to apply to the spawned object in the Z direction
    public float AmountOfForceToApplyZ;


    //Array to hold when the wall pieces are removed
    public int[] ArrayOfWhenToRemoveWallPieces;

    public static int RemoveWallCounter;

    //Possible spawn positions for the objects
    public System.Collections.Generic.List<GameObject> ObjectSpawnPositionObjects = new System.Collections.Generic.List<GameObject>();


    //Variable to hold the position of where to spawn the object
    public Vector3 ObjectSpawnPosition;

    //Variable to hold which random position to go to in the ObjectSpawnPositionObjects List
    public int RandomSpawnPosition;

    //Variable to determine whether to give vertical, horizontal or both speeds to red cube
    public int VerticalBoolValue;

    public int HorizontalBoolValue;

    //Array to hold all the red cubes for destruction at the end of the round
    public System.Collections.Generic.List<GameObject> AllRedCubesSpawned = new System.Collections.Generic.List<GameObject>();

    //Variable to hold the currently spawned object
    [HideInInspector]
    public GameObject CurrentlySpawnedObject;

    [HideInInspector]
    public Rigidbody CurrentlySpawnedObjectRigidbody;

    public GameObject CanvasGameObject;

    // Use this for initialization
    void Start () {



        

    }
	
	// Update is called once per frame
	void Update () {

        

    }

    public void SpawnObject(Vector3 SpawnPosition)
    {
        Debug.Log("Spawning Object.");

        //Determine randomly whether to give the ball vertical, horizontal or both speeds
        VerticalBoolValue = Random.Range(0, 2);
        HorizontalBoolValue = Random.Range(0, 2);

        //Spawn the red cube
        CurrentlySpawnedObject = Object.Instantiate(ObjectToSpawn, SpawnPosition, ObjectToSpawn.transform.localRotation);
        CurrentlySpawnedObjectRigidbody = CurrentlySpawnedObject.GetComponent<Rigidbody>();

        SpawnBallScript SpawnBallScript = GameObject.Find("Main Camera").GetComponent<SpawnBallScript>();

        CatchBallScript CatchBallScript = GameObject.Find("Main Camera").GetComponent<CatchBallScript>();

        AllRedCubesSpawned.Add(CurrentlySpawnedObject);

        if (VerticalBoolValue == 1 && HorizontalBoolValue == 0)
        {
            //Add force in the X direction specified to the red cube
            CurrentlySpawnedObjectRigidbody.AddForce(AmountOfForceToApplyX, 0, 0, ForceMode.Impulse);
            //Debug.Log("Add force in X Direction");
        }
        else if(VerticalBoolValue == 0 && HorizontalBoolValue == 1)
        {
            //Add force in the Y direction specified to the red cube
            CurrentlySpawnedObjectRigidbody.AddForce(0, AmountOfForceToApplyY, 0, ForceMode.Impulse);
            //Debug.Log("Add force in Y Direction");
        }
        else if(VerticalBoolValue == 1 && HorizontalBoolValue == 1)
        {
            //Add force in the X & Y direction specified to the red cube
            CurrentlySpawnedObjectRigidbody.AddForce(AmountOfForceToApplyX, AmountOfForceToApplyY, 0, ForceMode.Impulse);
            //Debug.Log("Add force in X & Y Direction");
        }
        else
        {
            //Add force in the X direction specified to the red cube
            CurrentlySpawnedObjectRigidbody.AddForce(AmountOfForceToApplyX, 0, 0, ForceMode.Impulse);
            //Debug.Log("Add force in X Direction");
        }
        


    }

}
