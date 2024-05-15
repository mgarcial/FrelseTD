using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float baseFireRate = 2f;
    [SerializeField] private float fireRate;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private ParticleSystem shootVFX;
    [SerializeField] private Transform firePoint;

    private float _fireRateCooldown;
    public float GetFireRate() => fireRate;

    void Awake()
    {
        fireRate = baseFireRate;
        EventManager.instance.OnAcceptEngineerEvent += ChangeFireRate;
    }

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
            shootVFX.Play();
            Projectile projectile = projectileFired.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.SetTarget(target);
            }
        }
    }

    public void ChangeFireRate(float multiplier)
    {
        fireRate /= multiplier;
        Debug.Log($"Im gettin this {fireRate} now");
    }

}
