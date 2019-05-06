using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public GameObject Panel1 = null;
    public GameObject Panel2 = null;
    private Animator p1Anim = null;
    private Animator p2Anim = null;

    private bool Locked = true;

    public GameObject light1, light2;
    public Material[] lightMats;



    // Start is called before the first frame update
    void Start()
    {
        p1Anim = Panel1.GetComponent<Animator>();
        p2Anim = Panel2.GetComponent<Animator>();

        light1.GetComponent<Renderer>().material = lightMats[0];
        light2.GetComponent<Renderer>().material = lightMats[0];

    }

    private void Update()
    {
        if (Locked)
        {
            light1.GetComponent<Renderer>().material = lightMats[0];
            light2.GetComponent<Renderer>().material = lightMats[0];
        }
        else
        {
            light1.GetComponent<Renderer>().material = lightMats[1];
            light2.GetComponent<Renderer>().material = lightMats[1];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Locked)
        {
            light1.GetComponent<Renderer>().material = lightMats[1];
            light2.GetComponent<Renderer>().material = lightMats[1];

            Debug.Log("ENTER");
            if (other.gameObject.tag == "Player")
            {
                p2Anim.SetTrigger("open");
                p1Anim.SetTrigger("open");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!Locked)
        {
            light1.GetComponent<Renderer>().material = lightMats[1];
            light2.GetComponent<Renderer>().material = lightMats[1];
            Debug.Log("EXIT");
            if (other.gameObject.tag == "Player")
            {
                p2Anim.SetTrigger("close");
                p1Anim.SetTrigger("close");
            }
        }
    }

    public void setLockTrue()
    {
        Locked = true;
    }
    public void setLockFalse()
    {
        Locked = false;
    }
    public bool getLock()
    {
        return Locked;
    }

}
