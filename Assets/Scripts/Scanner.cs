using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float rangeScan = 10f;
    private Transform _target;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scan()
    {
        Transform enemyTargeted = null;

        /*Aqui vienen referencias al script que maneje  los enemigos dentro de una lista, que recorra la lista de enemigos y calcule la distancia hacia cada uno, cuando la detecte más cercana lo elige como target
        foreach(Enemy in enemies)
        {
            float currentDistance = 

            if(enemy actualmente null y estan en rango)
                enemyTargetted = enemy position;
        }
        
        */
    }
    public bool TargetFound()
    {
        return _target != null;
    }

    public Transform GetTarget()
    {
        return _target;
    }


}
