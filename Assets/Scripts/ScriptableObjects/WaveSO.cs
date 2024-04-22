using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSO : ScriptableObject
{
    public List<Enemigo> enemiesInWave;
    public List<Transform> pathOfWave;
    public int spawnPoint;
}
