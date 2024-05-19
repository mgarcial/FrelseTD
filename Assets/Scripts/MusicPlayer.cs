using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.volume = Preferences.GetVolume();
    }

    public void SetMusicPlayerVolume(float volume) => audioSource.volume = volume;
}
