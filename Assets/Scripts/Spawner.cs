using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private Enemigo[] enemyPrefabs = new Enemigo[6];

    private Dictionary<string, Enemigo> enemies = new Dictionary<string, Enemigo>();
    private Transform trsfrm;

    private string[] enemyPrueba = { "ProTank", "ProTank", "Basic", "Helicarrier" };

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

    public void InstanteWave()
    {
        foreach(string enemy in enemyPrueba)
        {
            Instantiate(enemies[enemy], trsfrm.position, Quaternion.identity);
        }
    }
}
