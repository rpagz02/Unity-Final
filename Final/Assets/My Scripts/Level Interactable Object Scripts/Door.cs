using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Panel1 = null;
    public GameObject Panel2 = null;

    private Animator p1Anim = null;
    private Animator p2Anim = null;


    // Start is called before the first frame update
    void Start()
    {
        p1Anim = Panel1.GetComponent<Animator>();
        p2Anim = Panel2.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER");
        if(other.gameObject.tag == "Player")
        {
            p2Anim.SetTrigger("open");
            p1Anim.SetTrigger("open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT");
        if (other.gameObject.tag == "Player")
        {
            p2Anim.SetTrigger("close");
            p1Anim.SetTrigger("close");
        }
    }
}
