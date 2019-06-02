using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Panel1 = null;
    public GameObject Panel2 = null;

    private Animator p1Anim = null;
    private Animator p2Anim = null;

    private AudioSource audioSource;
    public AudioClip OpenSFX, CloseSFX;
    private bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (Panel1 != null && Panel2 != null)
        {
            p1Anim = Panel1.GetComponent<Animator>();
            p2Anim = Panel2.GetComponent<Animator>();
        }
    }


    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
                p1Anim.SetBool("OPEN", true);
                p2Anim.SetBool("OPEN", true);
                audioSource.PlayOneShot(OpenSFX, 0.5f);          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            p1Anim.SetBool("OPEN", false);
            p2Anim.SetBool("OPEN", false);
            audioSource.PlayOneShot(CloseSFX, 0.5f);
        }
    }
}
