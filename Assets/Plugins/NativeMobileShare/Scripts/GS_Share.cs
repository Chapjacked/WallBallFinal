namespace GameSlyce
{
    using UnityEngine;
    using System.Collections;

    using System.IO;
    using UnityEngine.UI;

    public class GS_Share : MonoBehaviour
    {
        public string shareMessage = "My Excellent Sharing Demo for Android an iOS";
        public GameObject[] includeInScreenshot, hideFromScreenshot;
        public bool includeLink;
        public string urliOS, urlAndroid;

        [Space(10)]
        public Animator _anim;
        public GameObject shareUI;
        public Image screenshotPreview;
        //Screenshot caputring process is asynchronous one
        public void TakeScreenshot()
        {
            if (File.Exists(path)) File.Delete(path);
            string s = shotName;
#if UNITY_EDITOR
            s = path;
#endif
            var includeStates = new bool[includeInScreenshot.Length];
            var hideStates = new bool[hideFromScreenshot.Length];

            int i = 0;
            
            foreach (var item in includeInScreenshot)
            {
                includeStates[i++] = item.activeSelf;
                item.SetActive(true);
            }

            i = 0;
            
            foreach (var item in hideFromScreenshot)
            {
                hideStates[i++] = item.activeSelf;
                item.SetActive(false);
            }
            
            ScreenCapture.CaptureScreenshot(s);

            i = 0;
            
            // Restore object states
            foreach (var item in includeInScreenshot)
            {
                item.SetActive(includeStates[i++]);
            }

            i = 0;
            
            foreach (var item in hideFromScreenshot)
            {
                item.SetActive(hideStates[i++]);
            }
            
            ShareScreenshot();
        }

        string shotName = "WallBallScreenshot.png";

        public string path
        {
            get
            {
                return
                //string.Format("{0}/{1}", Application.persistentDataPath, shotName);
                Path.Combine(Application.persistentDataPath, shotName);
            }
        }

        bool canClick = false;

        public void ZoomInInterface()
        {
            if (!canClick)
            {
                canClick = true;
                _anim.Play("zoomIn");
                return;
            }
            if (canClick)
            {
                canClick = false;
                _anim.Play("zoomOut");
                ShareScreenshot();
            }

        }

        IEnumerator SetPreview()
        {
            while (!File.Exists(path))
            {
                yield return new WaitForSeconds(.05f);
            }
            //print(">>>>>>>>>>>>>>");
            string path1 = "file:///" + path;
            WWW www = new WWW(path1);
            yield return www;
            Texture2D tex = www.texture;
            //print("Texture is" + tex.ToString());
            if (tex)
                screenshotPreview.sprite = Sprite.Create(www.texture, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
            
//            foreach (var item in includeInScreenshot)
//            {
//                item.SetActive(false);
//            }
            
            foreach (var item in hideFromScreenshot)
            {
                item.SetActive(true);
            }
        }

        public void ShareScreenshot()
        {
            StartCoroutine(ShareRoutine());
        }

        public void Show(bool show)
        {
            shareUI.SetActive(show);
        }
        //Just a very little delay to Make sure you have the file ready and accessible.
        IEnumerator ShareRoutine()
        {
            while (!File.Exists(path))
            {
                yield return new WaitForSeconds(.05f);
            }
            GSNativeShare.Share(shareMsg, path, includeLink ? _url : "");
        }

        string _url
        {
            get
            {
#if UNITY_IOS
                return urliOS;
#elif UNITY_ANDROID
                return urlAndroid;
#else
                return  "Not a correct Platform";
#endif
            }
        }
        public string shareMsg
        {
            get
            {
                if (includeLink)
                {
                    return string.Format("{0} Get it From {1}", shareMessage, _url);
                }
                else
                {
                    return shareMessage;
                }
            }
        }
        private static GS_Share _instance;
        public static GS_Share Instance
        { get { if (_instance == null) _instance = GameObject.FindObjectOfType<GS_Share>(); return _instance; } }
    }
}