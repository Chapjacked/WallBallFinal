using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallHitWallScript : MonoBehaviour
{

    //Other scripts
    public CatchBallScript CatchBallScript;
    public ScoringScript ScoringScript;

    //Audio Source Component for Back wall
    public AudioSource BackWallSFXSource;

    //Audio Source Clip for back wall
    public AudioClip BackWallSFXClip;

    private void Awake()
    {
        BackWallSFXSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ball")
        {

            //Sound Effects for hitting the back wall
            BackWallSFXSource.PlayOneShot(BackWallSFXClip, 0.5f);

        }
    }


}
