using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMusicPauser : MonoBehaviour
{
    public float transitionTime = 2f;
    //AudioSource playerSource;
    public AudioSource newSource;
    public AudioSource currentSource;
    AudioClip newSong;
    AudioClip currentSong;

    //This script is an alternate version of RoomMusicTransition but instead of playing the clips anew it pauses and continues them.
    //This script requires the script MusicToPlay to have a "musicSource" AudioSource variable;

    // Start is called before the first frame update
    void Start()
    {
        currentSource = GetComponent<AudioSource>();
        currentSong = currentSource.clip;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Music")
        {
            //newSource = collision.GetComponent<MusicToPlay>().musicSource;
            newSong = collision.GetComponent<MusicToPlay>().roomMusic;
            if (newSource != currentSource && newSong != currentSong)
            {
                currentSource.Pause();
                currentSource = newSource;
                currentSong = newSong;
                currentSource.Play();
            }
        }
    }
}
