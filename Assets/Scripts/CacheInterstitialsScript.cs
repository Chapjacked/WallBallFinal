using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartboostSDK;

public class CacheInterstitialsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Chartboost.cacheInterstitial(CBLocation.Default);
        Chartboost.cacheRewardedVideo(CBLocation.MainMenu);

        /** Cache rewarded video pre-roll message and video ad at location Main Menu. 
        See /assets/chartboost/scripts/chartboost.cs for available location options. **/

        Chartboost.cacheRewardedVideo(CBLocation.Default);



    }

    // Update is called once per frame
    void Update () {
		
	}
}
