using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SwitchMainCamera : MonoBehaviour
{

    public GameObject CutScene;
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<PlayableDirector>().state != PlayState.Playing)
        {
            Player.SetActive(true);
            Destroy(CutScene);
        }
    }
}
