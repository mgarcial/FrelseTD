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
            levelManager.LoadLevelSelector();
        }
    }
}
