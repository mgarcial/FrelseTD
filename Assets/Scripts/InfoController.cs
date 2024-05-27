using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    public RectTransform lore;
    public float endPosition;

    public void Update()
    {
        if (lore.anchoredPosition.y >= endPosition)
        {
            levelManager.LoadLevelSelectionScene();
        }
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            levelManager.LoadLevelSelectionScene();
        }
    }
}
