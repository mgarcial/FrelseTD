using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public delegate void EnemyKilledDelegate(int amount);
    public event EnemyKilledDelegate OnEnemyKilledEvent;

    public delegate void AllEnemiesDeadDelegate();
    public event AllEnemiesDeadDelegate OnAllEnemiesDeadEvent;

    private List<Enemigo> enemies;

    private static EnemyManager _instance;
    public static EnemyManager GetInstance()
    {
        if (_instance == null){
            _instance = new EnemyManager();
        }

        return _instance;
    }

    private EnemyManager() => enemies = new List<Enemigo>();

    public void AddEnemy(Enemigo enemy) => enemies.Add(enemy);

    public void RemoveEnemy(Enemigo enemy)
    {
        enemies.Remove(enemy);
        OnEnemyKilledEvent?.Invoke(enemy.GetGold());
    }
     // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
