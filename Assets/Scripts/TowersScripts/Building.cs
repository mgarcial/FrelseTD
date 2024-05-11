using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int cost;

    private Scanner _scanner;
    private Weapon _weapon;
    public int rate;
    

    GameManager gm = GameManager.instance;

    private void Awake()
    {
        gm.Circuitos = rate;
        _weapon = GetComponent<Weapon>();
        _scanner = GetComponentInChildren<Scanner>();
        Debug.Log("Scanner found: " + (_scanner != null));
    }

    private void Start()
    {
        InvokeRepeating("ScanEnemies", 0f, 0.1f);
        //Debug.Log("paso scan");

    }
 
    private void Update()
    {
        if(_scanner.TargetFound())
        {
            _weapon.Shoot(_scanner.GetTarget());
        }
    }

    private void ScanEnemies()
    {
        _scanner.Scan();
    }

    public int GetCost()
    {
        return cost;
    }
    public void DestroyTower()
    {
        Destroy(gameObject);
    }
}
