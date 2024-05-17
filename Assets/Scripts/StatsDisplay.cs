using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class StatsDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject prefab;
    public DamageDeal damageDeal;
    public GameObject panel;
    public Projectile projectile;
    public Text stats;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(prefab != null)
        {
            panel.SetActive(true);
            string statsInfo = GetStatsInfo();
            stats.text = statsInfo;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        stats.text = "";
        panel.SetActive(false);
    }
    private string GetStatsInfo()
    {
        string statsInfo = "";


        Building building = prefab.GetComponentInChildren<Building>();
        if(building != null)
        {
            statsInfo += "Price: " + building.GetCost() + "\n";
        }

        Weapon weapon = prefab.GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            statsInfo += "Fire Rate: " + weapon.GetFireRate() + "\n";
        }

        if(damageDeal != null)
        {
            statsInfo += "Damage: " + damageDeal.GetDamage() + "\n";
        }

        if(projectile != null)
        {
            if(projectile.StunsEnemy)
            {
                statsInfo += "Paralyze the enemy" + "\n"; 
            }

            if(projectile.BurnsEnemy)
            {
                statsInfo += "Makes the enemy burn" + "\n";
            }
        }

        return statsInfo;
    }
}
