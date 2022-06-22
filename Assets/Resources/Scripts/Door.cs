using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool flag = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            flag = true;
            //Написать код для добавления подказки
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            flag = false;
            //Написать код для удаления подсказки
        }
    }
    private void Update()
    {
        if (flag && PlayerController.key && Input.GetButtonDown("Fire1"))
        {
            if (Room.door)
            {
                Debug.Log("The door is closed");
                Room.door = false;
            }
            else
            {
                Debug.Log("The door is open");
                Room.door = true;
            }
        }
    }
}
