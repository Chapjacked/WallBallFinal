using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void RateUs()
    {
        #if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.TitanFlightStudios.WallBall");
#elif UNITY_IOS
        Application.OpenURL("https://itunes.apple.com/us/app/id1360193389?mt=8");
        print("Opened on iOS");
#endif


    }



    // Update is called once per frame
    void Update () {
		
	}
}
