using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private float _fireRateCooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_fireRateCooldown <= 0)
        {
            _fireRateCooldown = fireRate;
        }
        _fireRateCooldown -= Time.deltaTime;
    }

    public void Shoot(Transform target)
    {
        if (_fireRateCooldown <= 0)
        {
            GameObject projectileFired = Instantiate(projectilePrefab);
            Projectile projectile = projectileFired.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.SetTarget(target);
            }
        }
    }
}
