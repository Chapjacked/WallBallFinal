using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void RateUs()
    {
        Application.OpenURL("market://details?id=com.TitanFlightStudios.WallBall/");
    }



    // Update is called once per frame
    void Update () {
		
	}
}
