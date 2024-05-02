using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    //Wave atributes
    [SerializeField] private List<WaveSO> waves;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform endPoint;

    private int waveCounter;

    //others
    public delegate void EnemyKilledDelegate(int amount);
    public event EnemyKilledDelegate OnEnemyKilledEvent;

    public delegate void AllEnemiesDeadDelegate();
    public event AllEnemiesDeadDelegate OnAllEnemiesDeadEvent;

    [SerializeField] private List<Enemy> enemies;

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

        enemies = new List<Enemy>();
        waveCounter = 0;
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        //Debug.Log("Enemy added: " + enemy.name);
        //Debug.Log(enemies.Count);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        Debug.Log("Enemy killed: " + enemy.name);
        OnEnemyKilledEvent?.Invoke(enemy.GetCircuits());
        if (enemies.Count == 0)
        {
            OnAllEnemiesDeadEvent?.Invoke();
        }
    }

    public List<Enemy> GetEnemiesList() => enemies;

    public int GetEnemyCount() => enemies.Count;
    public void ClearEnemiesList() => enemies.Clear();

    //Para probar
    public void StartWave()
    {
        WaveSO nextWave = waves[waveCounter];

        StartCoroutine(StartWaveRoutine(1.0f, nextWave));
    }

    IEnumerator StartWaveRoutine(float seconds, WaveSO wave)
    {
        foreach (Enemy enemy in wave.enemiesOfWave)
        {
            enemy.endPoint = endPoint;
            Instantiate(enemy, spawnPoints[wave.spawnPoint].position, Quaternion.identity);
            //enemy.endPoint = endPoint;
            yield return new WaitForSeconds(seconds);
        }

        waveCounter++;
    }
}
