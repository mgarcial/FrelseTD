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

    [SerializeField] private int Event1Wave;
    [SerializeField] private int Event2Wave;
    [SerializeField] private int Event3Wave;

    private int waveCounter;

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
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        EventManager.instance.EnemyKilled(enemy.GetCircuits());
        if (waveCounter == waves.Count && enemies.Count == 0)
        {
            EventManager.instance.AllEnemiesDead();
        }
    }

    public List<Enemy> GetEnemiesList() => enemies;

    public int GetEnemyCount() => enemies.Count;
    public void ClearEnemiesList() => enemies.Clear();

    //Para probar
    public void StartWave()
    {
        if (waveCounter == (Event1Wave -1) || waveCounter == (Event2Wave -1) || waveCounter == (Event3Wave -1))
        {
            EventManager.instance.EventStart();
            EventManager.instance.TimeChange(TimeStates.pause);
        }

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
