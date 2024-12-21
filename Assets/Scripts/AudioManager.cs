using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource AudioSource;
    void Start()
    {
        Invoke("PlayMusic", 2f);
    }
    void PlayMusic()
    {
        AudioSource.Play();
    }

}
