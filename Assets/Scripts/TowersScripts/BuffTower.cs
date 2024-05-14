using UnityEngine;
using System.Collections.Generic;

public class BuffTower : Building
{
    [SerializeField] private float buffRadius = 10f;
    [SerializeField] private float fireRateMultiplier = 0.5f;

    private Dictionary<Weapon, bool> weaponsBuffed = new Dictionary<Weapon, bool>();

    private void Update()
    {
        List<Weapon> nearbyWeapons = FindNearbyWeapons();

        foreach (Weapon weapon in nearbyWeapons)
        {
            if (!weaponsBuffed.ContainsKey(weapon) || !weaponsBuffed[weapon])
            {
                weapon.ChangeFireRate(fireRateMultiplier);
                weaponsBuffed[weapon] = true;
            }
        }
    }

    private List<Weapon> FindNearbyWeapons()
    {
        List<Weapon> nearbyWeapons = new List<Weapon>();

        Weapon[] allWeapons = FindObjectsOfType<Weapon>();

        foreach (Weapon weapon in allWeapons)
        {
            if (Vector3.Distance(transform.position, weapon.transform.position) <= buffRadius)
            {
                nearbyWeapons.Add(weapon);
            }
            else
            {
                weaponsBuffed.Remove(weapon);
            }
        }

        return nearbyWeapons;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, buffRadius);
    }
}
