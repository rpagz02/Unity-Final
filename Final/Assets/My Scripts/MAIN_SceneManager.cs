using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MAIN_SceneManager : MonoBehaviour
{
    private AsyncOperation sceneAO;
    public GameObject[] SplashScreenObjects;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadSplashScreen()
    {

    }
    public void LoadMainMenuScreen()
    {

    }
    public void LoadLevel_0()
    {
        SplashScreenObjects[0].SetActive(false);
        SplashScreenObjects[1].SetActive(true);
        SplashScreenObjects[2].SetActive(true);
        StartCoroutine(LoadNewScene(1));
    }

    IEnumerator LoadNewScene(int sceneIndex)
    {
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            SplashScreenObjects[2].GetComponent<Text>().text = async.progress.ToString("F1");
            yield return null;
        }


    }
}