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
    //private Vector2 moveInput;
    //private Vector2 moveVelocity;
    
    public static bool glasses = false;
    public static bool key = false;

	//Animations and states
	Animator animator;
	string currentState;
	const string PLAYER_STAND = "Player_Idle";
	const string PLAYER_WALK_LEFT = "Player_Walk_Left";
	const string PLAYER_WALK_RIGHT = "Player_Walk_Right";
	const string PLAYER_WALK_UP = "Player_Walk_Up";
	const string PLAYER_WALK_DOWN = "Player_Walk_Down";

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
        //moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //moveVelocity = moveInput.normalized * speed;

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
			}

			else if (inputHorizontal < 0)
			{
				ChangeAnimationState(PLAYER_WALK_LEFT);
			}

			else if (inputVertical > 0)
			{
				ChangeAnimationState(PLAYER_WALK_UP);
			}

			else if (inputVertical < 0)
			{
				ChangeAnimationState(PLAYER_WALK_DOWN);
			}
		}
		else
		{
			rb.velocity = new Vector2(0f, 0f);
			ChangeAnimationState(PLAYER_STAND);
		}
        //rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
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
