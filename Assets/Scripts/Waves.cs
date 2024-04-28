using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private List<Enemigo> enemiesInWave;
    [SerializeField] private List<Transform> pathOfWave;
    [SerializeField] private int spawnPoint;

    public List<Enemigo> EnemiesInWave
    {
        get { return enemiesInWave; }
    }

    public List<Transform> PathOfWave
    {
        get 
        {
            return pathOfWave;
        }
    }

    public int SpawnPoint
    {
        get { return spawnPoint; }
    }
}
