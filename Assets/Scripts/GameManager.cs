using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using ChartboostSDK;
using CloudOnce;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour {

    //Other Scripts
    public BallBehaviorScript BallBehaviorScript;
    public CatchBallScript CatchBallScript;
    public RedCubeKillBallScript RedCubeKillBallScript;
    public KillBallScript KillBallScript;
    public ScoringScript ScoringScript;

    public int currentSkinIndex = 0;
    public int currency = 0;
    public int skinAvailability = 0;

    public Image SFXIcon;

    public Sprite MuteIcon;
    public Sprite UnMuteIcon;

    public GameObject SettingsPanel;

    private int playerAllTimeHighScore;
    
    public int PlayerAllTimeHighScore
    {
        get { return playerAllTimeHighScore; }
        set
        {
            playerAllTimeHighScore = value;
            
            // Report to CloudOnce
            Cloud.Leaderboards.SubmitScore(CloudIDs.LeaderboardIDs.GooglePlayLeaderboard, value);
        }
    }
    public Text PlayerAllTimeHighScoreText;

    [SerializeField]
    private Text playerNameText;
    
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

    private void OnEnable()
    {
        Cloud.OnSignedInChanged += OnSignInChanged;
    }

    private void OnDisable()
    {
        Cloud.OnSignedInChanged -= OnSignInChanged;
    }
    
    private void OnSignInChanged(bool arg0)
    {
        if (arg0)
        {
            Debug.LogFormat("Logged in user with id: {0}", Cloud.PlayerID);
            
            // set player name text
            playerNameText.text = string.Format("Welcome {0}", Cloud.PlayerDisplayName);
            Cloud.Leaderboards.LoadScores(CloudIDs.LeaderboardIDs.GooglePlayLeaderboard, OnLeaderboardDataReceived);
        }
        else
        {
            playerNameText.text = string.Empty;
        }
    }

    private void OnLeaderboardDataReceived(IScore[] obj)
    {
        Debug.LogFormat("Received {0} scores", obj.Length);
        
        if (obj.Length == 0)
        {
            return;
        }

        foreach (var s in obj)
        {
            Debug.LogFormat("Score {0} for playerId: {1} in leaderboardId: {2}", s.value, s.userID, s.leaderboardID);
        }
        
        // Find the playter's high score.
        var playerId = Cloud.PlayerID;

        var score = obj.FirstOrDefault(s => s.userID == playerId);

        if (score != null)
        {
            playerAllTimeHighScore = (int)score.value;
            PlayerAllTimeHighScoreText.text = PlayerAllTimeHighScore.ToString();
        }
    }

    public void UpdateHighScore()
    {
        PlayerPrefs.SetInt("HighScore", PlayerAllTimeHighScore);

        PlayerAllTimeHighScoreText.text = PlayerAllTimeHighScore.ToString();
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
