using UnityEngine;
using UnityEngine.AI;

public class PussyScript : MonoBehaviour
{
    public Transform[] moveSpots;
    public float waitTime;
    public int asd;
    public int dsa;
    
    
    private NavMeshAgent agent;
    private NavMeshAgent NPCagent;
    private int randomSpot;
    private Vector3 point;
    private Transform laserPoint;
    private int colorHeart = 0;
    
    private float startWaitTime = 3;
    private float laserTime = 10;
    private int rays = 90;
    private int distance = 20;
    private float angle = 360;
    
    private int status = 0;
    private bool blackLabel = false;
    //private bool glow = bool;
    
    /* Color Heart:
     * 0 - Нейтральный
     * 1 - Очарованный
     * 2 - Холодный*/
    
    /* Status
     * 0 - Патруль
     * 1 - Лазер
     * 2 - Заперт в комнате
     * 3 - Очарован*/
    
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
    const string NPC_STAND = "NPC_Idle";
    const string NPC_WALK_LEFT = "NPC_Walk_Left";
    const string NPC_WALK_RIGHT = "NPC_Walk_Right";
    const string NPC_WALK_UP = "NPC_Walk_Up";
    const string NPC_WALK_DOWN = "NPC_Walk_Down";

    public void Mark ()
    {
        blackLabel = true;
    }
    public void ChangeStatus(int val)
    {
        status = val;
    }
    private void Patrolling()
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
                    break;
                case (1):
                    Debug.Log(gameObject.name + ": We've already met");
                    break;
                case (2):
                    Debug.Log(gameObject.name + ": My heart bursts!");
                    colorHeart = 1;
                    break;
            }
        }
        else
        {
            Transform player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            if (RayToScan(player))
            {
                Debug.Log(gameObject.name + ": Ugh, he's a total player");
                colorHeart = 2;
                ChangeAnimationHeart(HEART_BLUE);
            }
        }

        blackLabel = false;
        status = 0;
    }
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

        if (status == 3) asd = 1;
        dsa = colorHeart;
        
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
        
        //For Animations
        if (agent.velocity.x == 0 && agent.velocity.y == 0)
        {
            
            ChangeAnimationState(NPC_STAND);
        }
        else if (agent.velocity.y > .5f)
        {
            ChangeAnimationState(NPC_WALK_UP);
        }
        else if (agent.velocity.y < -.5f)
        {
            ChangeAnimationState(NPC_WALK_DOWN);
        }
        else if (agent.velocity.x > .5f)
        {
            Debug.Log("Change");
            ChangeAnimationState(NPC_WALK_RIGHT);
        }
        else if (agent.velocity.x < -.5f)
        {
            ChangeAnimationState(NPC_WALK_LEFT);
        }
    }

    //Animaton heart changer
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
}
