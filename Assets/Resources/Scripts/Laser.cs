using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
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
        if (flag && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("The laser is activated");
            PussyScript.status = 1;
        }
    }
}
