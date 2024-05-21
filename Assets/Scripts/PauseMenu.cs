using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Update()
    {
        KeyboardInput();
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

    private void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
