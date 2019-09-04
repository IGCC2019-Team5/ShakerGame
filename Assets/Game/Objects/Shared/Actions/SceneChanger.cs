using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        if (this.gameObject.GetComponent<Button>())
            SoundManager.sm_Instance.PlayTap();
        SceneManager.LoadScene(sceneName);
    }
}
