using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip defaultClip;  // The default audio clip
    public AudioClip creditsClip;  // The audio clip for the credits scene

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
        audioSource.clip = defaultClip;
        audioSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip != newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }

    // Method called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Credits")  // Change to the name of your credits scene
        {
            ChangeMusic(creditsClip);
        }
        else
        {
            ChangeMusic(defaultClip);
        }
    }

    public void SetMusicPlayerVolume(float volume) => audioSource.volume = volume;
}
