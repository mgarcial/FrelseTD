using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool stunsEnemy = false;
    [SerializeField] private bool burnsEnemy = false;

    private Rigidbody2D _rb;
    private DamageDeal _damageDeal;
    private Transform _target;
    private TrailRenderer _trailRenderer;

    public bool StunsEnemy { get => stunsEnemy; set => stunsEnemy = value; }
    public bool BurnsEnemy { get => burnsEnemy; set => burnsEnemy = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _damageDeal = GetComponent<DamageDeal>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void FixedUpdate()
    {
        FireProjectile();
    }
    internal void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FireProjectile()
    {
        if (_target != null)
        {
            Enemy enemy = _target.GetComponent<Enemy>();
            if (enemy == null || enemy.IsDead())
            {
                Destroy(gameObject);
                return;
            }
            Vector2 direction = (_target.position - transform.position).normalized;

            _rb.velocity = direction * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            angle += 270f;

            transform.rotation = Quaternion.Euler(0, 0, angle);

            if (!_trailRenderer.enabled)
                _trailRenderer.enabled = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damageDeal.GetDamage());
            if (stunsEnemy)
            {
                enemy.Stunned();
            }

            if (burnsEnemy)
            {
                enemy.StartBurning();
            }

            Destroy(gameObject);
        }
    }

}
