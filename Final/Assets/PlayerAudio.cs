using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] MusicTracks;
    public AudioClip[] AmbientTracks;
    public AudioSource Music;
    public AudioSource Ambience;

    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        Music.PlayOneShot(MusicTracks[0], 0.12f);
        Ambience.PlayOneShot(AmbientTracks[0], 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
