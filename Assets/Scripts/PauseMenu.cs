using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject instructionsMenu;
    [SerializeField] private Toggle sfxToggle;

    private Button button;

    private void Update()
    {
        KeyboardInput();
    }
    private void Start()
    {
        sfxToggle.isOn = !AudioManager.GetInstance().IsSFXMuted();
        sfxToggle.onValueChanged.AddListener(OnSFXToggleChanged);
        button = gameObject.GetComponent<Button>();

        EventManager.instance.OnTimeChange += TimeStop;
    }

    private void OnDisable()
    {
        EventManager.instance.OnTimeChange -= TimeStop;
    }

    private void TimeStop(TimeStates timeStates)
    {
        switch (timeStates)
        {
            case TimeStates.pause:
                button.interactable = false;
                break;
            case TimeStates.unpause:
                button.interactable = true;
                break;
        }
    }

    public void Pause()
    {   
        AudioManager.GetInstance().PlayButtonPressed();
        instructionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        EventManager.instance.TimeChange(TimeStates.pause);
    }
    public void OpenInstructions()
    {
        instructionsMenu.SetActive(true);
    }
    public void CloseInstructions()
    {
        instructionsMenu.SetActive(false);
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
