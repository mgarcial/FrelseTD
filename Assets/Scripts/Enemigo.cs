using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private string enemyName;
    [SerializeField] private int circuits = 10;
    [SerializeField] private float maxHitPoints = 10;
    [SerializeField] private float speed;

    private float hitPoints;
    private Transform position;
    private EnemyManager _enemyManager;
    private NavMeshAgent navMeshAgent;
    private GameManager gameManager;
    private DamageDeal damageDeal;

    public Transform endPoint;
    public HealthbarBehavior healthBar;

    public string Name
    {
        get { return enemyName; }
    }

    public float HitPoints
    {
        get { return maxHitPoints; }
        set { maxHitPoints = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private void Awake()
    {
        position = GetComponent<Transform>();
        _enemyManager = EnemyManager.instance;
        gameManager = GameManager.instance;
        damageDeal = GetComponent<DamageDeal>();
    }

    void Start()
    {
        _enemyManager.AddEnemy(this);

        hitPoints = maxHitPoints;
        healthBar.SetHealth(hitPoints, maxHitPoints);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.updateRotation = false;

        navMeshAgent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(endPoint.position);

        Vector3 distance = position.position - endPoint.position;

        if (distance.magnitude <= 1.0f)
        {
            gameManager.TakeDamage(damageDeal.GetDamage());
            Die();
        }
    }

    public int GetCircuits() => circuits;
    internal void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        healthBar.SetHealth(hitPoints, maxHitPoints);
        Debug.Log("i took " + dmg +" and have " + hitPoints+ " left");
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
