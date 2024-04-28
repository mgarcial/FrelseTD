using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform trsfrm;
    private EnemyManager enemyManager;

    // Start is called before the first frame update
    void Awake()
    {
        trsfrm = GetComponent<Transform>();
        enemyManager = EnemyManager.instance;
        enemyManager.AddSpawner(this);
    }

    public void InstanteWave(List<Enemigo> wave, List<Transform> path) 
    {
        StartCoroutine(CreateEnemyRoutine(2f, wave, path));
    }

    IEnumerator CreateEnemyRoutine(float seconds, List<Enemigo> wave, List<Transform> path)
    {
        foreach (Enemigo enemy in wave)
        {
            Instantiate(enemy, trsfrm.position, Quaternion.identity);
            enemy.Path = path;
            yield return new WaitForSeconds(seconds);
        }
    }
}
