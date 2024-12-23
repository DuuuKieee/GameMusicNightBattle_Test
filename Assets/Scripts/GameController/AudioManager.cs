using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public static AudioManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Initialize()
    {
        Invoke("PlayMusic", GameConfig.DELAY_MUSIC);
    }
    void PlayMusic()
    {
        AudioSource.Play();
    }
    public void EndMusic()
    {
        AudioSource.Stop();
    }

}
