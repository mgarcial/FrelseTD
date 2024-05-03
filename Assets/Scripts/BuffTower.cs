using UnityEngine;
using System.Collections.Generic;

public class BuffTower : Building
{
    [SerializeField] private float buffRadius = 10f;
    [SerializeField] private float fireRateMultiplier = 2f;

    protected void Update()
    {

        List<Building> nearbyTowers = FindNearbyTowers();

        foreach (Building tower in nearbyTowers)
        {
            Weapon weapon = tower.GetComponentInChildren<Weapon>();
            if (weapon != null)
            {
                weapon.BuffFireRate(fireRateMultiplier);
                Debug.Log($"Im buffing {tower}");
            }
        }
    }

    private List<Building> FindNearbyTowers()
    {
        List<Building> nearbyTowers = new List<Building>();

        Building[] allTowers = FindObjectsOfType<Building>();

        foreach (Building tower in allTowers)
        {
            if (Vector3.Distance(transform.position, tower.transform.position) <= buffRadius)
            {
                nearbyTowers.Add(tower);
            }
        }

        return nearbyTowers;
    }
}
