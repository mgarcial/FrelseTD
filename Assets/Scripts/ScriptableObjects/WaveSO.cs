using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave")] public class WaveSO : ScriptableObject
{
    public List<Enemy> enemiesOfWave;
    public int spawnPoint;
}
