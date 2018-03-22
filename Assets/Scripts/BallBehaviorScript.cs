using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviorScript : MonoBehaviour {

    //Other scripts
    public RedCubeKillBallScript RedCubeKillBallScript;
    public SpawnBallScript SpawnBallScript;
    public KillBallScript KillBallScript;
    public CatchBallScript CatchBallScript;
    public ScoringScript ScoringScript;
    public SpawnMovingObjectScript SpawnMovingObjectScript;
    public GameManager GameManager;

    //Renderer for the ball
    public Renderer BallRenderer;

    //Array for the different materials to use for the ball (being catchable vs not catchable)
    public Material[] materials;

    //Player Camera variable
    public Camera PlayerCamera;

    //Variable to hold the string tag of the object being hit by the ball
    public string ObjectThatWasHit;

    //Audio Source Component for Ball hitting Walls
    public AudioSource BallSFXSource;

    //Audio Source Component for Miss
    public AudioClip MissedBallSFXClip;
    //Audio Source Component for hitting Hole
    public AudioClip HoleSFXClip;
    //Audio Source Component for hitting Red Cube
    public AudioClip RedCubeSFXClip;

    //Int to contain a random number each time the ball hits a wall
    public int numRandomBallSFX;

    //Int to hold how long the list of Ball SFX
    public int numNumberOfBallSFX;

    //Array for the Ball SFX
    public AudioClip[] arrayBallSFX;

    //Bool to determine whether to play SFX or not
    public static bool bShouldPlaySFX = true;

    //Bool to determin whether to play Music or not
    public static bool bShouldPlayMusic = true;

    public void Awake()
    {
        numNumberOfBallSFX = arrayBallSFX.Length;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "WallCatchable")
        {
            //Make it so that the ball is catchable
            CatchBallScript.isBallCatchable = true;
            //Debug.Log("Set ball as catchable" + CatchBallScript.isBallCatchable);

            SpawnBallScript.BallRigidbody.AddForce(-45, 0, 0, ForceMode.Impulse);
            Debug.Log("bShouldPlaySFX: " + bShouldPlaySFX);

            //Change the color of the light on the ball
            CatchBallScript.BallLight.color = Color.green;
            numRandomBallSFX = Random.Range(0, numNumberOfBallSFX);

            //Sound Effects for hitting the back wall
            BallSFXSource.PlayOneShot(arrayBallSFX[numRandomBallSFX], 0.5f);
            Debug.Log("Play SFX");

            //Change the material of the ball to make it visual that the ball is catchable
            BallRenderer.sharedMaterial = materials[1];



        }
        if (collision.gameObject.tag == "SideWall")
        {
                numRandomBallSFX = Random.Range(0, numNumberOfBallSFX);

                //Sound Effects for hitting the side wall
                BallSFXSource.PlayOneShot(arrayBallSFX[numRandomBallSFX], 0.5f);

        }
        if (collision.gameObject.tag == "Floor") 
        {
            numRandomBallSFX = Random.Range(0, numNumberOfBallSFX);

            //Sound Effects for hitting the floor
            BallSFXSource.PlayOneShot(arrayBallSFX[numRandomBallSFX], 0.5f);

            Debug.Log("Play Ball SFX Correctly");
        }

        if (collision.gameObject.tag == "Ceiling")
        {
            numRandomBallSFX = Random.Range(0, numNumberOfBallSFX);

            //Sound Effects for hitting the floor
            BallSFXSource.PlayOneShot(arrayBallSFX[numRandomBallSFX], 0.5f);

            Debug.Log("Play Ball SFX Correctly");
        }

    }

    
}
