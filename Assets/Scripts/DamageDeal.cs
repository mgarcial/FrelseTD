using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDeal : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    public int GetDamage() => damage;
}
