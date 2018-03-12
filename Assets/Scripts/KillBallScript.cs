using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBallScript : MonoBehaviour {

    //Other scripts
    public SpawnBallScript SpawnBallScript;
    public BallBehaviorScript BallBehaviorScript;
    public CatchBallScript CatchBallScript;
    public ScoringScript ScoringScript;

    //Audio Source Component for Back wall
    public AudioSource HoleSFXSource;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            ScoringScript.LaunchGameOverFunction();

            //Play sound effect for missing the ball
            HoleSFXSource.PlayOneShot(BallBehaviorScript.HoleSFXClip, 0.5f);

            CatchBallScript.isBallCatchable = false;

        }
    }

    public void DestroyObject(GameObject ObjectToDestroy)
    {
        Destroy(ObjectToDestroy);
        //Debug.Log("You missed the ball.");

    }
}
