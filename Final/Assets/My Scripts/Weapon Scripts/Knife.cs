using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        AnimHandling();

    }

    private void AnimHandling()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }
        if (Player.GetComponent<FPS_Controller>().GetSprintStatus() == true)
            GetComponent<Animator>().SetBool("isRunning", true);
        else
            GetComponent<Animator>().SetBool("isRunning", false);
    }
}
