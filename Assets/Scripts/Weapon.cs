using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private float _fireRateCooldown;

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
            GameObject projectileFired = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile projectile = projectileFired.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.SetTarget(target);
            }
        }
    }
}