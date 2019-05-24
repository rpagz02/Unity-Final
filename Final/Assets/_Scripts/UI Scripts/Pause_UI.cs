using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Pause_UI : MonoBehaviour
{

    [Header("[0] should be HUD, [1] should be Menu")]
    public GameObject[] Canvases;
    [Header("The Check Windows")]
    public GameObject CheckWindow_Restart;
    public GameObject CheckWindow_ExitMain;
    public GameObject CheckWindow_ExitDesktop;
    public GameObject[] SplashScreenObjects;
    [SerializeField]
    private bool Paused = false;
    private GameObject Player;
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    /// The Below Variables are used to gather temporary data from the Player to                     //
    /// pass to the other UI menus. (Current health this round, Current  armor this round.)          //
    ///  ----- The max health and other variables are more permenant and can only be changed by      //
    ///  ----- restarting the entire game. therefore can be stored and retrieved through playerPrefs //
    private float temp_PlayerHealth = 0;                                                             //
    private float temp_PlayerArmor = 0;                                                              //
    ///////////////////////////////////////////////////////////////////////////////////////////////////


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
            RefreshPlayerStats();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;            
            Canvases[0].SetActive(false);
            Canvases[1].SetActive(true);           
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
            Cursor.visible = false;
            Player.SetActive(true);

        }
    }

    #region OnClick Methods
    public void OnResume()
    {
        Paused = false;
    }
    public void OnRestart()
    {
        CheckWindow_Restart.SetActive(true);
    }
    public void OnMainMenu()
    {
        CheckWindow_ExitMain.SetActive(true);
    }
    public void OnDesktop()
    {
        CheckWindow_ExitDesktop.SetActive(true);
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

    #endregion OnClickMethods

    #region Data Transfer between UI methods
    void RefreshPlayerStats()
    {
        if (Player)
        {
            temp_PlayerArmor = Player.GetComponent<FPS_Player>().GetArmor();
            temp_PlayerHealth = Player.GetComponent<FPS_Player>().GetHealth();
        }
    }
    public float GetPlayerCurHealth()
    {
        return temp_PlayerHealth;
    }
    public float GetPlayerCurArmor()
    {
        return temp_PlayerArmor;
    }
    #endregion Data Transfer between UI methods
}




