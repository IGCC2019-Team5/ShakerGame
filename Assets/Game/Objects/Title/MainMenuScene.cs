using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : MonoBehaviour
{
    public void ExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://www.google.com/");
#else
        Application.Quit();
#endif
    }
}
