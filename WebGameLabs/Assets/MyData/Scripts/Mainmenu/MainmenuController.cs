using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainmenuController : MonoBehaviour
{
    public void OnPlayBtnClicked()
    {
        GameManager.LoadScene(Scenes.Gameplay);
    }

    public void OnQuitBtnClicked()
    {
        Debug.Log("Quitting Application!");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
