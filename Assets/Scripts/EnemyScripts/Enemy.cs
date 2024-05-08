using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int circuits = 10;
    [SerializeField] private float maxHitPoints = 10;
    [SerializeField] private float speed;
    [SerializeField] private float stunDuration = 1.0f;
    [SerializeField] private float burnDamagePerSecond = 2.0f;
    [SerializeField] private float burnDuration = 3.0f;

    private float hitPoints;
    private Transform pos;
    private EnemyManager _enemyManager;
    private NavMeshAgent navMeshAgent;
    private GameManager gameManager;
    private DamageDeal damageDeal;
    private bool isBurning = false;

    public Transform endPoint;
    public GameObject baseBuilding;
    public HealthbarBehavior healthBar;

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
        pos = GetComponent<Transform>();
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
        if (isBurning)
        {
            TakeBurnDamage();
        }
        navMeshAgent.SetDestination(endPoint.position);

        Vector2 distance = new(pos.position.x - endPoint.position.x, pos.position.y - endPoint.position.y);

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

    internal void Stunned()
    {
        navMeshAgent.speed = 0;
        StartCoroutine(RevertSpeedAfterStun());
    }

    IEnumerator RevertSpeedAfterStun()
    {
        yield return new WaitForSeconds(stunDuration);

        navMeshAgent.speed = speed;
    }

    public void StartBurning()
    {
        if (!isBurning)
        {
            isBurning = true;
            StartCoroutine(BurnTimer());
        }
    }

    private void TakeBurnDamage()
    {
        float damageThisFrame = burnDamagePerSecond * Time.deltaTime;
        hitPoints -= damageThisFrame;
        healthBar.SetHealth(hitPoints, maxHitPoints);

        if (hitPoints <= 0)
        {
            Die();
        }
    }
    private IEnumerator BurnTimer()
    {
        yield return new WaitForSeconds(burnDuration);
        isBurning = false;
    }
    public void Die()
    {
        _enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }
}
