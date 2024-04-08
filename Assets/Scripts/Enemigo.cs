using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private Transform nextPoint;
    [SerializeField] private int vida;

    private NavMeshAgent navMeshAgent;
    private bool empezar;

    void Start()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.updateRotation = false;
        empezar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (empezar)
        {
            navMeshAgent.SetDestination(nextPoint.position);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            empezar = true;
        }
    }

    internal void TakeDamage(object p)
    {
        throw new NotImplementedException();
    }
}
