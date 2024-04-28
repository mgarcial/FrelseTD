using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave")] public class WaveSO : ScriptableObject
{
    public List<Enemigo> enemiesOfWave;
    public int spawnPoint;
}
