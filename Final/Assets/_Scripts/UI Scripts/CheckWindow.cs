using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckWindow : MonoBehaviour
{
    // All of these are On Click methods that correspond to the Check Windows
    public GameObject GameCanvases;
    public GameObject LoadingCanvas;
    public GameObject LoadingText;
    [Space(10)]
    public GameObject[] CheckWindows;
    private int sceneIndex;

    public void OnRestart()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        GameCanvases.SetActive(false); // Everything else
        LoadingCanvas.SetActive(true); // Loading Canvas
        LoadingText.SetActive(true); // Text
        StartCoroutine(LoadNewScene(sceneIndex));
    }
    public void OnExitMain()
    {

        GameCanvases.SetActive(false);
        LoadingCanvas.SetActive(true);
        LoadingText.SetActive(true);
        StartCoroutine(LoadNewScene(0));
    }

    public void OnExitDesktop()
    {
        Application.Quit();
    }

    public void OnCancel()
    {
        for(int i = 0; i < CheckWindows.Length; i++)
        {
            CheckWindows[i].SetActive(false);
        }
    }
    IEnumerator LoadNewScene(int sceneIndex)
    {
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            LoadingText.GetComponent<Text>().text = (async.progress * 100).ToString("F2");
            yield return null;
        }
    }

}
