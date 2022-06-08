using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PussyScript : MonoBehaviour
{
    public Transform[] moveSpots;
    public float startWaitTime;

    private NavMeshAgent agent;
    private int randomSpot;
    public float waitTime;
    private Vector3 point;
    void Start()
    {
        waitTime = startWaitTime;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        randomSpot = Random.Range(0, moveSpots.Length);
    }
    
    void Update()
    {
        point = moveSpots[randomSpot].position;
        agent.SetDestination(point);

        if ((point - agent.transform.position).magnitude < 0.3f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
