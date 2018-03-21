using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartboostSDK;

public class ShowAdScript : MonoBehaviour {

    //Other scripts
    public ScoringScript ScoringScript;
    public CatchBallScript CatchBallScript;

    [HideInInspector]
    //variable to keep track of how many times the player failed to catch the ball
    public int numMissedCatches;
    public int numCurrentContinues = 0;
    public int numMaxContinues = 3;

    [HideInInspector]
    //variable to determine when to activate the ad
    public int numActivateAd;

	// Use this for initialization
	void Start () {


		
	}

    public void ShowAdFunction()
    {

        Debug.Log("Running Ad Function");

        if (numMissedCatches > 10)
        {

            Debug.Log("Misses higher than 5");

            numActivateAd = Random.Range(0, 100);

            if (numActivateAd < 30 + (2 * numMissedCatches))
            {
                // Show interstitial at location HomeScreen. 
                // See Chartboost.cs for available location options.
                //Chartboost.showRewardedVideo(CBLocation.MainMenu);
                if (PlayerPrefs.GetString("NoAdsShow") == "false")
                {
                    Chartboost.showInterstitial(CBLocation.Default);
                    print(PlayerPrefs.GetString("NoAdsShow"));
                    Debug.Log("Show Ad");
                    
                }
            }
        }

    }

    public void ShowRewardVideo()
    {

        if (Chartboost.hasRewardedVideo(CBLocation.MainMenu) == true && PlayerPrefs.GetString("NoAdsShow") == "false")
        {
            Debug.Log("Showing Reward Video");
            Chartboost.showRewardedVideo(CBLocation.MainMenu);
        }
        else
        {
            // We don't have a cached video right now, but try to get one for next time
            Chartboost.cacheRewardedVideo(CBLocation.MainMenu);
            Debug.Log("Does not have a video cached");
        }
    }

    public void ShowContinueVideo()
    {
        if (Chartboost.hasInterstitial(CBLocation.Default))
        {
            Debug.Log("Showing Continue Video");
            Chartboost.showInterstitial(CBLocation.Default);
        }
        else
        {
            // We don't have a cached video right now, but try to get one for next time
            Chartboost.cacheInterstitial(CBLocation.Default);
            Debug.Log("Does not have a video cached - caching one");
        }
        //Begin play again
        didCompleteRewardedVideo(CBLocation.Default, 0);
        

    }

    public void didDismissInterstitial(CBLocation Default)
    {
        Chartboost.didDismissInterstitial -= didDismissInterstitial;

    }

    public void didCloseInterstitial(CBLocation Default)
    {
        Chartboost.didCloseInterstitial -= didCloseInterstitial;

    }

    public void didCompleteRewardedVideo(CBLocation location, int reward)
    {

        Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
        if(reward == 0)
        CatchBallScript.EndOfRoundPanel.SetActive(false);

        numCurrentContinues += 1;

    }
    // Update is called once per frame
    void Update () {




    }
}
