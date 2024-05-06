using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void LoadGameScene(int lvl)
    {
        Preferences.SetCurrentLvl(lvl);
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
