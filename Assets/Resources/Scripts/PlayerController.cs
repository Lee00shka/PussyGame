using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
	
	//Components
    private Rigidbody2D rb;

	//Player
	float walkSpeed = 4f;
	float speedLimiter = 0.7f;
	float inputHorizontal;
	float inputVertical;

	private int direction = 1;

	public static bool glasses = false;
    public static bool key = false;

	//Animations and states
	Animator animator;
	string currentState;
	const string PLAYER_STAND_LEFT = "Player_Idle_Left";
	const string PLAYER_STAND_RIGHT = "Player_Idle_Right";
	const string PLAYER_WALK_LEFT = "Player_Walk_Left";
	const string PLAYER_WALK_RIGHT = "Player_Walk_Right";

    void Attack()
    {
        
    }
    public static void UseMirror()
    {
        glasses = true;
        Debug.Log("Я недел очки");
    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
		inputHorizontal = Input.GetAxisRaw("Horizontal");
		inputVertical = Input.GetAxisRaw("Vertical");

    }

    private void HeadDirection(int route)
    {
	    if (route == 1)
	    {
		    ChangeAnimationState(PLAYER_STAND_RIGHT);
	    }

	    else
	    {
		    ChangeAnimationState(PLAYER_STAND_LEFT);
	    } 
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
				direction = 1;
			}

			else if (inputHorizontal < 0)
			{
				ChangeAnimationState(PLAYER_WALK_LEFT);
				direction = 0;
			}
			
			else if (inputVertical != 0)
			{
				if (direction == 1)
				{
					ChangeAnimationState(PLAYER_WALK_RIGHT);
				}
				else
				{
					ChangeAnimationState(PLAYER_WALK_LEFT);
				}
			}
		}
		else
		{
			rb.velocity = new Vector2(0f, 0f);
			HeadDirection(direction);
		}
    }

	//Animaton state changer
	private void ChangeAnimationState(string newState)
	{
		//Stop animation from interrupting itself
		if (currentState == newState) return;
		
		//Play new animation
		animator.Play(newState);

		//Update currentState
		currentState = newState;		
	}
}
