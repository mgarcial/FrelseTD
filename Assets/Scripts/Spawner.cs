using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private Enemigo[] enemyPrefabs;

    private Dictionary<string, Enemigo> enemies = new Dictionary<string, Enemigo>();
    private Transform trsfrm;

    // Start is called before the first frame update
    void Awake()
    {
        trsfrm = GetComponent<Transform>();
        for(int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemies.Add(enemyPrefabs[i].Name, enemyPrefabs[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstanteWave(List<Enemigo> wave) 
    {
        StartCoroutine(CreateEnemyRoutine(2f, wave));
    }

    IEnumerator CreateEnemyRoutine(float seconds, List<Enemigo> wave)
    {
        foreach (Enemigo enemy in wave)
        {
            string enemyName = enemy.Name;
            Instantiate(enemies[enemyName], trsfrm.position, Quaternion.identity);
            yield return new WaitForSeconds(seconds);
        }
    }
}
