using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Animation
    private const string OPEN = "Open";
    private const string CLOSE = "Close";
    
    private string currentDoor;
    private Animator doorAnimator;
    private int flag;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        ChangeDoorState(OPEN);
        flag += 1;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        flag -= 1;
        if (flag == 0)
        {
            ChangeDoorState(CLOSE); 
        }
    }
    
    private void ChangeDoorState(string newDoor)
    {
        //Stop animation from interrupting itself
        if (currentDoor == newDoor) return;
		
        //Play new animation
        doorAnimator.Play(newDoor);

        //Update currentState
        currentDoor = newDoor;
    }
    
    private void Start()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
    }
}
