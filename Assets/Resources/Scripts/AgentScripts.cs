using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScripts : MonoBehaviour
{
    [SerializeField] Transform target;

    private NavMeshAgent agent;
    private GameObject[] abc;
    
    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;

        
        
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    void Update()
    {
        agent.SetDestination(target.position);
    }

}
