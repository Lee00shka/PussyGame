using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetButton("Fire1"))
            {
                if (PlayerController.key)
                {
                    if (Room.door)
                    {
                        Room.door = false;
                    }
                    else
                    {
                        Room.door = true;
                    }
                }
            }
        }
    }
}
