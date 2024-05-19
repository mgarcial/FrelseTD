using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Slider volumeSlider;

    private MusicPlayer musicPlayer;

    private void Awake()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
    }

    private void Start()
    {
        sfxToggle.isOn = Preferences.GetToggleSfx();
        sfxToggle.onValueChanged.AddListener(ToggleChange);

        volumeSlider.minValue = 0;
        volumeSlider.maxValue = 1;
        volumeSlider.value = Preferences.GetVolume();
        volumeSlider.onValueChanged.AddListener(MusicVolumeChange);
    }

    private void ToggleChange(bool isOn)
    {
        Preferences.ToggleSfx(isOn);
    }

    private void MusicVolumeChange(float volume)
    {
        Preferences.SetVolume(volume);
        musicPlayer.SetMusicPlayerVolume(volume);
    }
}
