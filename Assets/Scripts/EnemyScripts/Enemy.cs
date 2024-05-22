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
    [SerializeField] private ParticleSystem deathVFX;

    private float hitPoints;
    private Transform pos;
    private EnemyManager _enemyManager;
    private NavMeshAgent navMeshAgent;
    private GameManager gameManager;
    private DamageDeal damageDeal;
    private bool isBurning = false;
    private bool isActive = true;
    private float inactivityTimer = 0f;
    private float inactivityThreshold = 5f;

    public Transform endPoint;
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
    public bool IsDead()
    {
        return hitPoints <= 0;
    }

    private void Awake()
    {
        pos = GetComponent<Transform>();
        _enemyManager = EnemyManager.instance;
        gameManager = GameManager.instance;
        damageDeal = GetComponent<DamageDeal>();
        AudioManager.GetInstance().PlayEnemySpawn();
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
        if (isActive)
        {
            if (isBurning)
            {
                TakeBurnDamage();
            }
            navMeshAgent.SetDestination(endPoint.position);

            Vector2 distance = new Vector2(pos.position.x - endPoint.position.x, pos.position.y - endPoint.position.y);

            if (distance.magnitude <= 1.0f)
            {
                gameManager.TakeDamage(damageDeal.GetDamage());
                circuits = 0;
                Die();
            }
        }
        else
        {
            inactivityTimer += Time.deltaTime;

            if (inactivityTimer >= inactivityThreshold)
            {
                DestroyGameObject();
            }
        }
    }


    public int GetCircuits() => circuits;

    internal void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        healthBar.SetHealth(hitPoints, maxHitPoints);
        if (hitPoints <= 0)
        {
            Die();
            AudioManager.GetInstance().PlayEnemyDeath();
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
    public void SetActive(bool active)
    {
        isActive = active;

        if (isActive)
        {
            inactivityTimer = 0f;
        }
    }
    public void Die()
    {
        deathVFX.Play();
        _enemyManager.RemoveEnemy(this);
        deathVFX.transform.parent = null;
        gameObject.SetActive(false);
        SetActive(false);
        Invoke("DestroyGameObject", deathVFX.main.duration); 
    }

    private void DestroyGameObject()
    {
        Destroy(deathVFX.gameObject);
        Destroy(gameObject);
    }
}
