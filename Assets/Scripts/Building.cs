using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int cost;
    public int rate;

    GameManager gm = GameManager.instance;

    private void Awake()
    {
        gm.SumGoldRate = rate;
    }
}
