using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainmenuController : MonoBehaviour
{
    [SerializeField] private Transform titleText;

    private void Start()
    {
        StartCoroutine(AnimateTitle());
    }

    private IEnumerator AnimateTitle()
    {

        titleText.localScale = Vector3.zero;

        yield return new WaitForSeconds(3f);

        while (titleText.localScale != Vector3.one)
        {
            titleText.localScale = Vector3.Lerp(titleText.localScale, Vector3.one, Time.deltaTime);
            yield return null;
        }
    }

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
