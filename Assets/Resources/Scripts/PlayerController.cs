using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
	
	//Components
    private Rigidbody2D rb;

	//Walk
	float walkSpeed = 4f;
	float speedLimiter = 0.7f;
	float inputHorizontal;
	float inputVertical;

	
	//Game property
	public static bool _glasses = false;
	public bool glasses = false;
    public static bool key = false;

	//Animations and states
	private int direction = 1;
	Animator animator;
	string currentState;
	const string PLAYER_STAND_LEFT = "Player_Idle_Left";
	const string PLAYER_STAND_RIGHT = "Player_Idle_Right";
	const string PLAYER_WALK_LEFT = "Player_Walk_Left";
	const string PLAYER_WALK_RIGHT = "Player_Walk_Right";

	Animator glassesAnimator;
	private string currentGlasses;
	private const string GLASSES_LEFT = "Left_Glasses";
	private const string GLASSES_RIGHT = "Right_Glasses";
	
	
	
	//Game mechanics
	public static void WearGlasses()
    {
        _glasses = true;
    }
    
    //Animaton
    private void ChangeAnimationState(string newState)
    {
	    //Stop animation from interrupting itself
	    if (currentState == newState) return;
		
	    //Play new animation
	    animator.Play(newState);

	    //Update currentState
	    currentState = newState;		
    }
    
    private void ChangeAnimationGlasses(string newGlasses)
    {
	    Debug.Log("We change glasses animation! " + currentGlasses + "," + newGlasses);
	    //Stop animation from interrupting itself
	    if (currentGlasses == newGlasses) return;
		
	    //Play new animation
	    glassesAnimator.Play(newGlasses);

	    //Update currentState
	    currentGlasses = newGlasses;		
    }
    private void HeadDirection(int route)
    {
	    if (route == 1)
	    {
		    ChangeAnimationState(PLAYER_STAND_RIGHT);
		    if (glasses)
		    {
			    ChangeAnimationGlasses(GLASSES_RIGHT);
		    }
	    }

	    else
	    {
		    ChangeAnimationState(PLAYER_STAND_LEFT);
		    if (glasses)
		    {
			    ChangeAnimationGlasses(GLASSES_LEFT);
		    }
	    } 
    }
    //Standart
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();
		glassesAnimator = GameObject.Find("Glasses").GetComponent<Animator>();
    }
    private void Update()
    {
		inputHorizontal = Input.GetAxisRaw("Horizontal");
		inputVertical = Input.GetAxisRaw("Vertical");
		glasses = _glasses;

    }
    private void FixedUpdate() 
    {
		if (inputHorizontal != 0 || inputVertical != 0)
		{
			if (inputHorizontal != 0 && inputVertical != 0)
			{
				inputHorizontal *= speedLimiter;
				inputVertical *= speedLimiter;
			}
			rb.velocity = new Vector2(inputHorizontal * walkSpeed, inputVertical * walkSpeed);

			if (inputHorizontal > 0)
			{
				ChangeAnimationState(PLAYER_WALK_RIGHT);
				if (glasses)
				{
					ChangeAnimationGlasses(GLASSES_RIGHT);
				}
				direction = 1;
			}

			else if (inputHorizontal < 0)
			{
				ChangeAnimationState(PLAYER_WALK_LEFT);
				if (glasses)
				{
					ChangeAnimationGlasses(GLASSES_LEFT);
				}
				direction = 0;
			}
			
			else if (inputVertical != 0)
			{
				if (direction == 1)
				{
					ChangeAnimationState(PLAYER_WALK_RIGHT);
					if (glasses)
					{
						ChangeAnimationGlasses(GLASSES_RIGHT);
					}
				}
				else
				{
					ChangeAnimationState(PLAYER_WALK_LEFT);
					if (glasses)
					{
						ChangeAnimationGlasses(GLASSES_LEFT);
					}
				}
			}
		}
		else
		{
			rb.velocity = new Vector2(0f, 0f);
			HeadDirection(direction);
		}
    }

	
}
