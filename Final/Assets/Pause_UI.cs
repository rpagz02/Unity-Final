using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Pause_UI : MonoBehaviour
{
    public GameObject[] Canvases;
    // [0] should always remain as the HUD Canvas
    // [1] should always remain as the Menu Canvas
    [SerializeField]
    private bool Paused = false;
    public GameObject[] SplashScreenObjects;
    private GameObject Player;

    private void Start()
    {
        Paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<FPS_Player>().IsDead() == false)
            PauseHandler();
        else if (Player.GetComponent<FPS_Player>().IsDead() == true)
        {
            Canvases[0].SetActive(false);
            Canvases[1].SetActive(true);
            Player.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void PauseHandler()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused = !Paused;
        }

        if (Paused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (Canvases[0].activeInHierarchy && !Canvases[1].activeInHierarchy)
            {
                Canvases[0].SetActive(false);
                Canvases[1].SetActive(true);
            }
            Time.timeScale = 0;
            Player.SetActive(false);
        }
        else if (!Paused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (!Canvases[0].activeInHierarchy && Canvases[1].activeInHierarchy)
            {
                Canvases[0].SetActive(true);
                Canvases[1].SetActive(false);
            }
            Time.timeScale = 1;
            Player.SetActive(true);

        }
    }

    // ON CLICK METHODS

    public void OnResume()
    {
        Paused = false;
    }

    public void OnRestart()
    {
        //SceneManager.LoadScene(1);
        SplashScreenObjects[0].SetActive(false); // Everything else
        SplashScreenObjects[1].SetActive(true); // Loading Canvas
        SplashScreenObjects[2].SetActive(true); // Text
        StartCoroutine(LoadNewScene(1));
    }

    public void OnMainMenu()
    {
        //SceneManager.LoadScene(0);
        SplashScreenObjects[0].SetActive(false);
        SplashScreenObjects[1].SetActive(true);
        SplashScreenObjects[2].SetActive(true);
        StartCoroutine(LoadNewScene(0));
    }
    public void OnDesktop()
    {
        Application.Quit();
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




