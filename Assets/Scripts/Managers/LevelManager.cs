using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadGameScene(int lvl)
    {
        SceneManager.LoadScene("Level " + lvl);
    }

    public void LoadLevelSelectionScene() => SceneManager.LoadScene("Level selector");

    public void LoadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void LoadNextLvlScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void LoadOptionsScene() => SceneManager.LoadScene("Options");

    public void LoadMainMenuScene() => SceneManager.LoadScene("Main Screen");

    public void QuitGame()
    {
        Application.Quit();
    }
}
