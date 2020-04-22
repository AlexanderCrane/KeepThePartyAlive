using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] music;
    AudioSource audioSource;
    AudioListener audioListener;
    // Start is called before the first frame update
    void Start()
    {
       audioListener = GetComponent<AudioListener>();
       audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying){
            audioSource.clip = music[Random.Range(0, music.Length)];
            audioSource.Play();
        }
    }
}
