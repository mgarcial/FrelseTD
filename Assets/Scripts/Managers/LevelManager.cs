using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

<<<<<<< Updated upstream
    // Update is called once per frame
    void Update()
    {
        
=======
    public void LoadLevelSelectionScene() => SceneManager.LoadScene("Level selector");

    public void LoadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void LoadNextLvlScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void LoadOptionsScene() => SceneManager.LoadScene("Options");

    public void LoadMainMenuScene() => SceneManager.LoadScene("Main Screen");

    public void QuitGame()
    {
        Application.Quit();
>>>>>>> Stashed changes
    }
}
