using UnityEngine;
using UnityEngine.AI;

public class PussyScript : MonoBehaviour
{
    public Transform[] moveSpots;
    public float waitTime;

    //Movement
    private NavMeshAgent agent;
    private int randomSpot;
    private Vector3 point;
    private Transform laserPoint;

    //Vision
    private float startWaitTime = 3;
    private float laserTime = 10;
    private int rays = 90;
    private int distance = 20;
    private float angle = 360;
    
    //Charm
    private int status = 0;
    private bool blackLabel = false;
    private int colorHeart = 0;
    private bool tagGlasses = false;
    /* Color Heart:
     * 0 - Нейтральный
     * 1 - Очарованный
     * 2 - Холодный*/
    
    /* Status
     * 0 - Патруль
     * 1 - Лазер
     * 2 - Заперт в комнате
     * 3 - Очарован*/
    
    //Animation
    private int direction = 1;
    //private bool glow = bool;
    Animator animator;
    private Animator NPCanimator;
    
    //Animation for hearts
    string currentState;
    string currentHeart;
    const string HEART_NONE = "None";
    const string HEART_BLUE = "Blue_Heart";
    const string HEART_RED = "Red_Heart";
    const string HEART_BLACK = "Black_Heart";
    
    //Animation for NPC
    const string NPC_STAND_LEFT = "NPC_Idle_Left";
    const string NPC_STAND_RIGHT = "NPC_Idle_Right";
    const string NPC_WALK_LEFT = "NPC_Walk_Left";
    const string NPC_WALK_RIGHT = "NPC_Walk_Right";

    //Changing private properties
    public void Mark ()
    {
        blackLabel = true;
    }
    public void ChangeStatus(int val)
    {
        status = val;
    }
    public void PlayerPutOnGlasses()
    {
        if (colorHeart == 2)
        {
            colorHeart = 0;
        }
    }
    
    //Game mechanics
    private void Patrolling()
    {
        point = moveSpots[randomSpot].position;
        agent.SetDestination(point);

        if ((point - agent.transform.position).magnitude < 0.9f)
        {
            Debug.Log("WTF");
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
    private void Laser()
    {
        if (RayToScan(laserPoint))
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
                    //Добавить анимацию игры с лазером
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
    private void RoamInRoom()
    {
        if (Room.box.bounds.Contains(transform.position))
        {
            //Не дописанная механика
            print("I'm in the room");
        }
    }
    private void Attacked()
    {
        if (blackLabel)
        {
            switch (colorHeart)
            {
                case (0):
                    Debug.Log(gameObject.name + ": My heart bursts!");
                    colorHeart = 1;
                    ChangeAnimationHeart(HEART_RED);
                    Global.numOfEnchanted += 1;
                    break;
                case (1):
                    if (tagGlasses == PlayerController.glasses)
                    {
                        Debug.Log(gameObject.name + ": We've already met");
                    }
                    else
                    {
                        Debug.Log(gameObject.name + ": I already with another");
                    }
                    break;
                case (2):
                    Debug.Log(gameObject.name + ": Okay, but this is the last time");
                    colorHeart = 1;
                    Global.numOfEnchanted += 1;
                    break;
            }
        }
        else
        {
            Transform player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            if (colorHeart != 0 && RayToScan(player))
            {
                if (colorHeart == 1)
                {
                    Debug.Log(gameObject.name + ": Ugh, he's a total player");
                    colorHeart = 2;
                    ChangeAnimationHeart(HEART_BLUE);
                    Global.numOfEnchanted -= 1;
                }
                else
                {
                    Debug.Log(gameObject.name + "YOU SHELL NOT LIVE");
                    Global.EndGame();
                }
            }
        }

        blackLabel = false;
        status = 0;
    }
    
    //Support 
    private bool GetRaycast(Vector3 dir, Transform target)
    {
        bool result = false;
        RaycastHit2D hit = new RaycastHit2D();
        Vector3 pos = transform.position;
        hit = Physics2D.Raycast(pos, dir, distance);
        Vector3 hitt = hit.point;

        if (hit.collider != null)
        {
            if (hit.transform == target)
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
    private bool RayToScan(Transform target)
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
            if (GetRaycast(dir, target)) a = true;

            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector3(-x, y, 0));
                if (GetRaycast(dir, target)) b = true;
            }
        }
        if (a || b) result = true;
        return result;
    }
    
    //Animation
    private void ChangeAnimationHeart(string newHeart)
    {
        //Stop animation from interrupting itself
        if (currentHeart == newHeart) return;
		
        //Play new animation
        animator.Play(newHeart);

        //Update currentHeart
        currentHeart = newHeart;		
    }
    private void ChangeAnimationState(string newState)
    {
        //Stop animation from interrupting itself
        if (currentState == newState) return;
		
        //Play new animation
        NPCanimator.Play(newState);

        //Update currentState
        currentState = newState;		
    }
    private void AnimationRun()
    {
        if (agent.velocity.x == 0 && agent.velocity.y == 0)
        {
            if (direction == 1)
            { 
                ChangeAnimationState(NPC_STAND_RIGHT); 
            }
            
            else
            { 
                ChangeAnimationState(NPC_STAND_LEFT); 
            }
            
        }
        else if (agent.velocity.x > .5f)
        {
            ChangeAnimationState(NPC_WALK_RIGHT);
            direction = 1;
        }
        else if (agent.velocity.x < -.5f)
        {
            ChangeAnimationState(NPC_WALK_LEFT);
            direction = 0;
        }
        else if (agent.velocity.y != 0)
        {
            if (direction == 1)
            {
                ChangeAnimationState(NPC_WALK_RIGHT); 
            }

            else
            {
                ChangeAnimationState(NPC_WALK_LEFT);
            }
        }
    }
    
    //Standart 
    private void Start()
    {
        laserPoint = GameObject.FindGameObjectsWithTag("Laser")[0].transform;
        waitTime = startWaitTime;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        randomSpot = Random.Range(0, moveSpots.Length);
        animator = transform.GetChild(0).GetComponent<Animator>();
        NPCanimator = GetComponent<Animator>();
    }
    private void Update()
    {
        switch (status)
        {
            case 0: Patrolling();
                break;
            case 1: Laser();
                break;
            case 2: RoamInRoom();
                break;
            case 3: Attacked();
                break;
        }
        AnimationRun();
    }
}
