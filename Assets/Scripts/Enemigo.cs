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
    [SerializeField] private int hitPoints = 10;
    [SerializeField] private float speed;

    private EnemyManager _enemyManager;
    private NavMeshAgent navMeshAgent;
    private bool start;

    public string Name
    {
        get { return enemyName; }
    }

    public int HitPoints
    {
        get { return hitPoints; }
        set { hitPoints = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
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
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            navMeshAgent.SetDestination(nextPoint.position);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            start = true;
        }
    }

    public int GetCircuits() => circuits;
    internal void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0)
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
