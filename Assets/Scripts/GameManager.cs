using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChartboostSDK;
using CloudOnce;

public class GameManager : MonoBehaviour {

    //Other Scripts
    public BallBehaviorScript BallBehaviorScript;
    public CatchBallScript CatchBallScript;
    public RedCubeKillBallScript RedCubeKillBallScript;
    public KillBallScript KillBallScript;
    public ScoringScript ScoringScript;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int currentSkinIndex = 0;
    public int currency = 0;
    public int skinAvailability = 0;

    public Image SFXIcon;

    public Sprite MuteIcon;
    public Sprite UnMuteIcon;

    public GameObject SettingsPanel;

    public int PlayerAllTimeHighScore;
    public Text PlayerAllTimeHighScoreText;

    public GameObject PlayerAllTimeHighScoreGameObject;




    public void Awake()
    {

        PlayerPrefs.SetInt("HighScore", PlayerAllTimeHighScore);


        Debug.Log("Caching Video");
        Chartboost.cacheRewardedVideo(CBLocation.MainMenu);
        Chartboost.cacheInterstitial(CBLocation.Default);

        SFXIcon = SFXIcon.gameObject.GetComponent<Image>();

        if (SFXIcon.sprite == MuteIcon)
        {
            //Mute sounds
            AudioListener.volume = 0;
        }
        else if (SFXIcon.sprite == UnMuteIcon)
        {
            //UnMute sounds
            AudioListener.volume = 1;
        }

        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(instance);

        //disable settings panel by default
        SettingsPanel.SetActive(false);


        PlayerAllTimeHighScoreText = PlayerAllTimeHighScoreGameObject.GetComponent<Text>();


        if (PlayerPrefs.HasKey("HighScore"))
        {
            PlayerAllTimeHighScore = PlayerPrefs.GetInt("HighScore");

        }
        else
        {
            PlayerPrefs.SetInt("HighScore", PlayerAllTimeHighScore);

            PlayerAllTimeHighScore = PlayerPrefs.GetInt("HighScore");
        }

        PlayerAllTimeHighScoreText.text = PlayerAllTimeHighScore.ToString();

        Save();

        PlayerAllTimeHighScoreText.text = CloudVariables.HighScore.ToString();

        Cloud.Storage.Save();

    }


    public void Update()
    {
        
    }

    public void UpdateHighScore()
    {
        PlayerPrefs.SetInt("HighScore", PlayerAllTimeHighScore);

        PlayerAllTimeHighScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        Save();
    }

    public void ToggleMuteSFX()
    {
        if (SFXIcon.sprite == UnMuteIcon)
        {
            //Mute all SFX
            AudioListener.volume = 0.0f;

            //Change Sound Icon
            SFXIcon = SFXIcon.gameObject.GetComponent<Image>();

            SFXIcon.sprite = MuteIcon;

        }
        else
        {
            //Unmute all SFX
            AudioListener.volume = 1.0f;

            //Change Sound Icon
            SFXIcon = SFXIcon.gameObject.GetComponent<Image>();

            SFXIcon.sprite = UnMuteIcon;

        }
    }

    public void ConnectGameCenterAccount()
    {
        //Connect GameCenter stuff here
    }

    public void ConnectGooglePlayAccount()
    {
        //Connect Google Play stuff here
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }
}
