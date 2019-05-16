using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MAIN_SceneManager : MonoBehaviour
{
    private AsyncOperation sceneAO;
    [Header("Drag and drop the Menu Canvases here")]
    public GameObject SplashCanvas;
    public GameObject[] SplashObjects;
    public GameObject LoadingIcon;
    public GameObject LoadingText;
    public GameObject MainMenuCanvas;



    // Start is called before the first frame update
    void Start()
    {
    }

    public void LoadMainMenuScreen()
    {
        SplashObjects[0].GetComponent<Animator>().SetTrigger("Animate");
        SplashCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }
    public void LoadLevel_0()
    {
        MainMenuCanvas.SetActive(false);
        SplashCanvas.SetActive(false);
        LoadingIcon.SetActive(true);
        StartCoroutine(LoadNewScene(1));
    }

    IEnumerator LoadNewScene(int sceneIndex)
    {
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            LoadingText.GetComponent<Text>().text = (async.progress * 100).ToString("F1");
            yield return null;
        }


    }
}