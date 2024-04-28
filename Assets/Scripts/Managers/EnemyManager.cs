using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    //Wave atributes
    [SerializeField] private List<Waves> waves;
    [SerializeField] private List<Spawner> spawnPoints;

    private int waveCounter;

    //others
    public delegate void EnemyKilledDelegate(int amount);
    public event EnemyKilledDelegate OnEnemyKilledEvent;

    public delegate void AllEnemiesDeadDelegate();
    public event AllEnemiesDeadDelegate OnAllEnemiesDeadEvent;

    [SerializeField] private List<Enemigo> enemies;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        enemies = new List<Enemigo>();
        waveCounter = 0;
    }

    public void AddEnemy(Enemigo enemy)
    {
        enemies.Add(enemy);
        //Debug.Log("Enemy added: " + enemy.name);
        //Debug.Log(enemies.Count);
    }

    public void RemoveEnemy(Enemigo enemy)
    {
        enemies.Remove(enemy);
        Debug.Log("Enemy killed: " + enemy.name);
        OnEnemyKilledEvent?.Invoke(enemy.GetCircuits());
        if (enemies.Count == 0)
        {
            OnAllEnemiesDeadEvent?.Invoke();
        }
    }

    public void AddSpawner(Spawner spawn)
    {
        spawnPoints.Add(spawn);
    }

    public void AddWave(Waves wave)
    {
        waves.Add(wave);
    }

    public List<Enemigo> GetEnemiesList() => enemies;

    public int GetEnemyCount() => enemies.Count;
    public void ClearEnemiesList() => enemies.Clear();

    //Para probar
    public void StartWave()
    {
        Waves nextWave = waves[waveCounter];
        spawnPoints[nextWave.SpawnPoint].InstanteWave(nextWave.EnemiesInWave, nextWave.PathOfWave);
    }
}
