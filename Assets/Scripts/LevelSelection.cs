using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    private Button[] levelButtons;
    private int highestLevel;

    // Start is called before the first frame update
    void Start()
    {
        highestLevel = Preferences.GetMaxLvl();

       levelButtons = GetComponentsInChildren<Button>();
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > highestLevel)
            {
                levelButtons[i].interactable = false;
            }
        }

    }
    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene("Level " + levelNum);
    }

}
