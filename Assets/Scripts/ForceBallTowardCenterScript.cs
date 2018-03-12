using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBallTowardCenterScript : MonoBehaviour {

    //Other scripts
    public SpawnBallScript SpawnBallScript;
    public BallBehaviorScript BallBehaviorScript;
    public CatchBallScript CatchBallScript;
    public ScoringScript ScoringScript;
    public RotateWallScript RotateWallScript;
    public HitSideWallRightScript HitSideWallRightScript;
    public HitSideWallLeftScript HitSideWallLeftScript;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (RotateWallScript.isWallRotating == false)
        {
           // Debug.Log("didBallHitSideWallRight: " + HitSideWallRightScript.didBallHitSideWallRight);
            if (HitSideWallRightScript.didBallHitSideWallRight == true)
            {
                if (collision.gameObject.tag == "Ball")
                {
                    collision.rigidbody.AddForce(-2, 0, 0, ForceMode.Impulse);
                    //Debug.Log("didBallHitSideWallRight: " + HitSideWallRightScript.didBallHitSideWallRight);
                }
            }
            else if (HitSideWallLeftScript.didBallHitSideWallLeft == true)
            {
                if (collision.gameObject.tag == "Ball")
                {
                    collision.rigidbody.AddForce(2, 0, 0, ForceMode.Impulse);
                    //Debug.Log("didBallHitSideWallRight: " + HitSideWallRightScript.didBallHitSideWallRight);
                }
            }
        }
    }
}
