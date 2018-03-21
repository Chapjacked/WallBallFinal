using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAdsManager : MonoBehaviour {

 

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetString("NoAdsShow", "false");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
