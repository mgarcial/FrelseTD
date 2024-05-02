using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D _rb;
    private DamageDeal _damageDeal;
    private Transform _target;
    private TrailRenderer _trailRenderer;
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
            Vector2 direction = (_target.position - transform.position).normalized;

            // Set velocity based on the calculated direction and speed
            _rb.velocity = direction * speed;

            // Calculate rotation angle to point towards the target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            angle += 270f;

            // Apply rotation to the arrow's transform (only rotate around Z-axis)
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
        Enemigo enemy = collision.gameObject.GetComponent<Enemigo>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damageDeal.GetDamage());
            Debug.Log("i hit" + enemy);
            Destroy(gameObject);
        }
    }

}
