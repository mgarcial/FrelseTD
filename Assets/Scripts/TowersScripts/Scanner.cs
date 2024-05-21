using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class Scanner : MonoBehaviour
{
    public float rangeScan = 10f;
    public GameObject rangeIndicator;

    private EnemyManager _enemyManager;
    private Transform _target;
    private bool isMouseOver;

    private void Awake()
    {
        _enemyManager = EnemyManager.instance;
    }

    void Start()
    {
        rangeIndicator.transform.localScale = new Vector3(rangeScan * 2, rangeScan * 2, 1);
    }


    public void Scan()
    {
        Transform enemyTargeted = null;
        List<Enemy> enemies = _enemyManager.GetEnemiesList();

        foreach (Enemy enemy in enemies)
        {
            float currentDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if (enemyTargeted == null && currentDistance <= rangeScan)
            {
                enemyTargeted = enemy.transform;
            }
        }

        _target = enemyTargeted;
    }

    public bool TargetFound()
    {
        return _target != null;
    }

    public Transform GetTarget()
    {
        return _target;
    }
    private void OnMouseEnter()
    {
        isMouseOver = true;
        if (rangeIndicator != null)
        {
            rangeIndicator.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
        if (rangeIndicator != null)
        {
            rangeIndicator.SetActive(false);
        }
    }

}

