using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private Transform nextPoint;
    [SerializeField] private int gold = 10;

    private EnemyManager _enemyManager;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        _enemyManager = EnemyManager.GetInstance();
    }

    void Start()
    {
        _enemyManager.AddEnemy(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(nextPoint.position);
    }

    public int GetGold() => gold;
    internal void TakeDamage(object p)
    {
        throw new NotImplementedException();
    }
}
