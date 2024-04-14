using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private Transform nextPoint;
    [SerializeField] private string enemyName;
    [SerializeField] private int circuits = 10;
    [SerializeField] private int vida;

    private EnemyManager _enemyManager;
    private NavMeshAgent navMeshAgent;
    private bool empezar;

    public string Name
    {
        get { return enemyName; }
    }

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
        empezar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (empezar)
        {
            navMeshAgent.SetDestination(nextPoint.position);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            empezar = true;
        }
    }

    public int GetCircuits() => circuits;
    internal void TakeDamage(int dmg)
    {
        vida -= dmg;
        if (vida <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }
}
