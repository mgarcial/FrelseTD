using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/WaveSO")] public class WaveSO : ScriptableObject
{
    public List<Enemigo> enemiesInWave;
    public int spawnPoint;
}
