using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int cost;
    private Weapon _weapon;
    private bool _isHover = false;
    public int rate;
    

    GameManager gm = GameManager.instance;

    private void Awake()
    {
        gm.SumGoldRate = rate;
        _weapon = GetComponent<Weapon>();
    }

    private void Start()
    {
        
    }

    public void DestroyTower()
    {
        
    }
}
