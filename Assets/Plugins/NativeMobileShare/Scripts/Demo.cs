using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour {
    
    private void Awake()
    {
        GameSlyce.GS_Share.Instance.TakeScreenshot();
    }
}
