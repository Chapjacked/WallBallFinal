using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChartboostSDK;

public class LoadLevelScript : MonoBehaviour {

    //Other scripts
    public SpawnBallScript SpawnBallScript;
    public KillBallScript KillBallScript;
    public BallBehaviorScript BallBehaviorScript;
    //public ScoringScript ScoringScript;
    //public RotateWallScript RotateWallScript;
    //public HitSideWallRightScript HitSideWallRightScript;
    //public HitSideWallLeftScript HitSideWallLeftScript;
    //public SpawnMovingObjectScript SpawnMovingObjectScript;
    public CatchBallScript CatchBallScript;
    public RedCubeKillBallScript RedCubeKillBallScript;
    public GameManager GameManager;

    // Use this for initialization
    void Start() {

        //Starting of the app
        Screen.orientation = ScreenOrientation.Portrait;

        Chartboost.cacheRewardedVideo(CBLocation.MainMenu);

    }

    public void LoadLevelFunction(int LevelToLoad)
    {
        SceneManager.LoadScene(LevelToLoad);


        if (AudioListener.volume == 0)
        {
            //Set icon to mute icon
            GameManager.SFXIcon.sprite = GameManager.MuteIcon;
        }
        else
        {
            //Set icon to unmute icon
            GameManager.SFXIcon.sprite = GameManager.UnMuteIcon;
        }


    }

        public void ChangeBallSkin(int SkinToChangeTo)
    {
        //Change the material of the ball so that it is not catchable
        BallBehaviorScript.BallRenderer.sharedMaterial = BallBehaviorScript.materials[SkinToChangeTo];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
