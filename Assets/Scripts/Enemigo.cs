using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private string enemyName;
    [SerializeField] private int circuits = 10;
    [SerializeField] private int hitPoints = 10;
    [SerializeField] private float speed;

    private Transform position;
    private EnemyManager _enemyManager;
    private NavMeshAgent navMeshAgent;
    private List<Transform> path;
    private int nextPos;

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

    public List<Transform> NextPos
    {
        set { path = value; }
    }

    private void Awake()
    {
        position = GetComponent<Transform>();
        _enemyManager = EnemyManager.GetInstance();
        nextPos = 0;
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
        navMeshAgent.SetDestination(path[nextPos].position);

        Vector3 distance = position.position - path[nextPos].position;

        if (distance.magnitude <= 1.0f)
        {
            if(nextPos < path.Count)
            {
                nextPos++;
            }
            else{
                Die();
            }
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
