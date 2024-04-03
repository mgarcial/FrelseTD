using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int cost;
    private Weapon _weapon;
    private Scanner _scanner;
    private bool _isHover = false;
    public int rate;
    

    GameManager gm = GameManager.instance;

    private void Awake()
    {
        gm.SumGoldRate = rate;
        _weapon = GetComponent<Weapon>();
        _isHover = GetComponentInChildren<Scanner>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(!_isHover && _scanner.TargetFound())
        {
            _weapon.Shoot(_scanner.GetTarget());
        }
    }

    public void DestroyTower()
    {
        Destroy(gameObject);
    }
}
