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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _damageDeal = GetComponent<DamageDeal>();
    }

    private void FixedUpdate()
    {
        FireProjectile();
    }
    internal void SetTarget(Transform target)
    {
        throw new NotImplementedException();
    }

    private void FireProjectile()
    {
        if (_target != null)
        {
            transform.LookAt(_target.position);
            _rb.velocity = transform.forward * speed;
            Vector2 dir = _target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(-25, 0, angle);
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
            Destroy(gameObject);
        }
    }

}
