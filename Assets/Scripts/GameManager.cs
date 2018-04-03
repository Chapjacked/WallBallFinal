using System.Linq;
using System.Runtime.InteropServices;
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

    private static int playerAllTimeHighScore;
    
    /// <summary>
    /// Returns the player's all-time high score value.
    /// </summary>
    public static int PlayerAllTimeHighScore
    {
        get { return playerAllTimeHighScore; }
        set
        {
            if (value > playerAllTimeHighScore)
            {
                // Report to CloudOnce
                UpdateLeaderboardScore(value);
                
                playerAllTimeHighScore = value;

                UpdateHighScore();
            }
        }
    }

    private static void UpdateLeaderboardScore(int score)
    {
        var id = string.Empty;
        
        #if UNITY_EDITOR
       
        return;

        #elif UNITY_ANDROID
        id = CloudIDs.LeaderboardIDs.GooglePlayLeaderboard;
        #elif UNITY_IOS
        id = CloudIDs.LeaderboardIDs.iosleaderboard;
        #endif
        
        Cloud.Leaderboards.SubmitScore(id, score);
    }

    public string DefaultLeaderboardID
    {
        get
        {
            var id = "";

            #if UNITY_EDITOR

            return "";
            
            #elif UNITY_ANDROID
            id = CloudIDs.LeaderboardIDs.GooglePlayLeaderboard;
            #elif UNITY_IOS
            id = CloudIDs.LeaderboardIDs.iosleaderboard;
            #endif
    
            return id;
        }
    }
    
    public Text PlayerAllTimeHighScoreText;

    [SerializeField]
    private Text playerNameText;
    
    public GameObject PlayerAllTimeHighScoreGameObject;

    public void Awake()
    {
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

        //LoadStoredHighScoreValue();

        PlayerAllTimeHighScoreText.text = PlayerAllTimeHighScore.ToString();
        
        Cloud.Storage.Save();
        
        // Update player name

        if (Cloud.IsSignedIn)
        {
            playerNameText.text = string.Format("Welcome {0}", Cloud.PlayerDisplayName);
        }
    }

    /// <summary>
    /// Loads the previously known high score value so it can be immediately displayed.
    /// </summary>
    private void LoadStoredHighScoreValue()
    {
        //if (PlayerPrefs.HasKey("HighScore"))
        //{
        //    PlayerAllTimeHighScore = PlayerPrefs.GetInt("HighScore");
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("HighScore", PlayerAllTimeHighScore);
        //    Save();
        //}
    }

    private void OnEnable()
    {
        Cloud.OnSignedInChanged += OnSignInChanged;
    }

    private void OnDisable()
    {
        Cloud.OnSignedInChanged -= OnSignInChanged;
    }
    
    private void OnSignInChanged(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            Debug.LogFormat("Logged in user with id: {0}", Cloud.PlayerID);
            
            // set player name text
            playerNameText.text = string.Format("Welcome {0}", Cloud.PlayerDisplayName);
            
            Cloud.Leaderboards.LoadScores(DefaultLeaderboardID, OnLeaderboardDataReceived);
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
            PlayerAllTimeHighScore = (int)score.value;
            
            // Update the high score text
            PlayerAllTimeHighScoreText.text = PlayerAllTimeHighScore.ToString();
        }
    }
    
    public static void UpdateHighScore()
    {   
        //PlayerPrefs.SetInt("HighScore", PlayerAllTimeHighScore);
        //Save();
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

    private static void Save()
    {
        //PlayerPrefs.Save();
    }
}
