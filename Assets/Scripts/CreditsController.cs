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
            StartCoroutine(WaitAndLoadScene(2f));
        }
    }
    IEnumerator WaitAndLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        levelManager.LoadMainMenuScene();
    }
}
