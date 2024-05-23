using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    public RectTransform credits;
    public float endPosition;

    public void Update()
    {
        if(credits.anchoredPosition.y >= endPosition)
        {
            levelManager.LoadMainMenuScene();
        }
    }
}
