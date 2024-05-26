using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void LoadGameScene(int lvl)
    {
        AudioManager.GetInstance().PlayButtonPressed();
        Preferences.SetCurrentLvl(lvl);
        SceneManager.LoadScene("Level " + lvl);
    }

    public void LoadLevelSelectionScene()
    {
        AudioManager.GetInstance().PlayButtonPressed();
        SceneManager.LoadScene("Level selector");
    }

    public void LoadCurrentScene()
    {
        AudioManager.GetInstance().PlayButtonPressed();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadIntroScene()
    {
        AudioManager.GetInstance().PlayButtonPressed();
        SceneManager.LoadScene("Intro");
    }


    public void LoadNextLvlScene()
    {
        AudioManager.GetInstance().PlayButtonPressed();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadOptionsScene()
    {
        AudioManager.GetInstance().PlayButtonPressed();
        SceneManager.LoadScene("Options");
    }

    public void LoadMainMenuScene() 
    {
        AudioManager.GetInstance().PlayButtonPressed();
        SceneManager.LoadScene("Main Screen");
    }

    public void LoadLevelSelector()
    {
        AudioManager.GetInstance().PlayButtonPressed();
        SceneManager.LoadScene("Level selector");
    }
    public void LoadCreditsScene() 
    {
        AudioManager.GetInstance().PlayButtonPressed();
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        AudioManager.GetInstance().PlayButtonPressed();
        Application.Quit();
    }
}
