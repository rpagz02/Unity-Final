using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Audio_Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        Button b = GetComponent<Button>();
        AudioSource audio = GetComponent<AudioSource>();
        b.onClick.AddListener(delegate () { audio.Play(); });
    }

}
