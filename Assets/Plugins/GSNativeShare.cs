namespace GameSlyce
{
    using UnityEngine;
    using System.Collections;
#if UNITY_IOS
    using System.Runtime.InteropServices;
#endif
    public class GSNativeShare
    {
#if UNITY_IOS
	public struct GConfig
	{
		public string title;
		public string message;
	}

	[DllImport ("__Internal")] private static extern void showPopup(ref GConfig conf);

	public struct GSocialShare
	{
		public string text;
		public string url;
		public string image;
		public string subject;
	}

	[DllImport ("__Internal")] private static extern void showShare(ref GSocialShare conf);

	public static void ShowPopup(string title, string message)
	{
		GConfig conf = new GConfig();
		conf.title  = title;
		conf.message = message;
		showPopup(ref conf);
	}


	public static void ShowShare(string defaultTxt, string subject, string url, string img)
	{
		GSocialShare conf = new GSocialShare();
		conf.text = defaultTxt;
		conf.url = url;
		conf.image = img;
		conf.subject = subject;

		showShare(ref conf);
	}
#endif
        public static void Share(string shareText, string imagePath, string url, string subject = "")
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass thisJavaClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject thisJavaObject = new AndroidJavaObject("android.content.Intent");

            thisJavaObject.Call<AndroidJavaObject>("setAction", thisJavaClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + imagePath);
            thisJavaObject.Call<AndroidJavaObject>("putExtra", thisJavaClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            thisJavaObject.Call<AndroidJavaObject>("setType", "image/png");

            thisJavaObject.Call<AndroidJavaObject>("putExtra", thisJavaClass.GetStatic<string>("EXTRA_TEXT"), shareText);

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject chooser = thisJavaClass.CallStatic<AndroidJavaObject>("createChooser", thisJavaObject, subject);
            currentActivity.Call("startActivity", chooser);

#elif UNITY_IOS && !UNITY_EDITOR
		ShowShare(shareText, subject, url, imagePath);
#else
            Debug.Log("Currently Only iOS and Android Platforms are Supported!");
#endif
        }

    }
}