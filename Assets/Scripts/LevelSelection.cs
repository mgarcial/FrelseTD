using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] levelButtons;
    public Image Lock;
    public Image Done;
    private int highestLevel;

    // Start is called before the first frame update
    void Start()
    {
        highestLevel = PlayerPrefs.GetInt("highestLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelNum = i + 1;
            if (levelNum > highestLevel)
            {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().sprite = Lock.sprite;
                levelButtons[i].GetComponentInChildren<Text>().text = "";
            }
            else
            {
                levelButtons[i].interactable = true;
                levelButtons[i].GetComponentInChildren<Text>().text = "" + levelNum;
                levelButtons[i].GetComponent<Image>().sprite = Done.sprite;
            }
        }

    }
    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene("Level " + levelNum);
    }

}
