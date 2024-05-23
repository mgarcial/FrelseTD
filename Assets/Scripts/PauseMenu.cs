using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Toggle sfxToggle;

    private void Update()
    {
        KeyboardInput();
    }
    private void Start()
    {
        sfxToggle.isOn = !AudioManager.GetInstance().IsSFXMuted();
        sfxToggle.onValueChanged.AddListener(OnSFXToggleChanged);
    }
    public void Pause()
    {   
        AudioManager.GetInstance().PlayButtonPressed();
        pauseMenu.SetActive(true);
        EventManager.instance.TimeChange(TimeStates.pause);
    }

    public void Restart()
    {
        AudioManager.GetInstance().PlayButtonPressed();
        pauseMenu.SetActive(false);
        EventManager.instance.TimeChange(TimeStates.unpause);
    }

    public void MainMenu()
    {
        GameManager.instance.CleanLevel();
        AudioManager.GetInstance().PlayButtonPressed();
        SceneManager.LoadScene("Main Screen");
        EventManager.instance.TimeChange(TimeStates.unpause); ;
    }
    private void OnSFXToggleChanged(bool isOn)
    {
        AudioManager.GetInstance().SetSFXMute(!isOn);
    }

    private void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
