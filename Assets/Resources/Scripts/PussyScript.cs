using UnityEngine;
using UnityEngine.AI;

public class PussyScript : MonoBehaviour
{
    public Transform[] moveSpots;
    public float waitTime;
    
    private NavMeshAgent agent;
    private int randomSpot;
    private Vector3 point;
    private Transform laserPoint;

    private float startWaitTime = 3;
    private float laserTime = 10;
    private int rays = 90;
    private int distance = 20;
    private float angle = 360;
    public static int status = 0;
    //private int colorHeart = 0;
    
    /* 0 - Патруль
     * 1 - Лазер
     * 2 - Заперт в комнате */
     
    void Patrolling()
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
    void Laser()
    {
        if (RayToScan())
        {
            agent.SetDestination(laserPoint.position);
            if ((laserPoint.position - agent.transform.position).magnitude < 0.3f)
            {
                if (waitTime <= 0)
                {
                    status = 0;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else
            {
                waitTime = laserTime;
            }
        }
        else
        {
            status = 0;
        }
    } 
    void RoamInRoom()
    {
        if (Room.box.bounds.Contains(transform.position))
        {
            print("Я в комнате");
        }
    }
    bool GetRaycast(Vector3 dir)
    {
        bool result = false;
        RaycastHit2D hit = new RaycastHit2D();
        Vector3 pos = transform.position;
        hit = Physics2D.Raycast(pos, dir, distance);
        Vector3 hitt = hit.point;

        if (hit.collider != null)
        {
            if (hit.transform == laserPoint)
            {
                result = true;
                Debug.DrawLine(pos, hitt, Color.green);
            }
            else
            {
                Debug.DrawLine(pos, hitt, Color.blue);
            }
        }
        else
        {
            Debug.DrawLine(pos, dir * distance, Color.red);
        }

        return result;
    }
    bool RayToScan()
    {
        bool result = false;
        bool a = false;
        bool b = false;
        float j = 0;
        for (int i = 0; i < rays; i++)
        {
            float x = Mathf.Sin(j); 
            float y = Mathf.Cos(j);

            j += angle * Mathf.Deg2Rad / rays;

            Vector3 dir = transform.TransformDirection(new Vector3(x, y, 0));
            if (GetRaycast(dir)) a = true;

            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector3(-x, y, 0));
                if (GetRaycast(dir)) b = true;
            }
        }
        if (a || b) result = true;
        return result;
    }
    void Start()
    {
        laserPoint = GameObject.FindGameObjectsWithTag("Laser")[0].transform;
        waitTime = startWaitTime;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        randomSpot = Random.Range(0, moveSpots.Length);
    }
    void Update()
    {
        RoamInRoom();
        RayToScan();
        switch (status)
        {
            case 0: Patrolling();
                break;
            case 1: Laser();
                break;
            case 2: RoamInRoom();
                break;
        }
    }
}
