using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Enemigo[][] waves;

    public delegate void EnemyKilledDelegate(int amount);
    public event EnemyKilledDelegate OnEnemyKilledEvent;

    public delegate void AllEnemiesDeadDelegate();
    public event AllEnemiesDeadDelegate OnAllEnemiesDeadEvent;

    [SerializeField]private List<Enemigo> enemies;

    private static EnemyManager _instance;
    public static EnemyManager GetInstance()
    {
        if (_instance == null) {
            _instance = new EnemyManager();
        }

        return _instance;
    }

    private EnemyManager() => enemies = new List<Enemigo>();

    public void AddEnemy(Enemigo enemy)
    {
        enemies.Add(enemy);
        Debug.Log("enemy added"+ enemy);
    }
    public void RemoveEnemy(Enemigo enemy)
    {
        enemies.Remove(enemy);
        OnEnemyKilledEvent?.Invoke(enemy.GetCircuits());
        if (enemies.Count == 0)
        {
            OnAllEnemiesDeadEvent?.Invoke();
        }
    }

    public List<Enemigo> GetEnemiesList() => enemies;

    public int GetEnemyCount() => enemies.Count;
    public void ClearEnemiesList() => enemies.Clear();
   
}
