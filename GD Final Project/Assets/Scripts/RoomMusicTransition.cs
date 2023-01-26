using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMusicTransition : MonoBehaviour
{
    public float transitionTime = 2f;
    AudioClip currentSong;
    AudioClip newSong;

    // Start is called before the first frame update
    void Start()
    {
        currentSong = GetComponent<AudioSource>().clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Music")
        {
            newSong = collision.GetComponent<MusicToPlay>().roomMusic;
            if (newSong != currentSong)
            {
                currentSong = newSong;
                GetComponent<AudioSource>().Pause();
                GetComponent<AudioSource>().clip = currentSong;

                GetComponent<AudioSource>().Play();
            }
        }
    }
}
