using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{   
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;
    public AudioClip music4;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!source.isPlaying) {
            PlayMusic();
        }
    }

    void PlayMusic() {
        float num = Random.Range(0.0f, 4.0f);

        if(num > 3) {
            source.clip = music4;
        }
        else if(num > 2) {
            source.clip = music3;
        }
        else if(num > 1) {
            source.clip = music2;
        }
        else {
            source.clip = music1;
        }

        source.Play();
    }
}
