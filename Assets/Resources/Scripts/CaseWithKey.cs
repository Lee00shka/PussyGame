using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseWithKey : MonoBehaviour
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
            if (PlayerController.glasses)
            {
                Debug.Log("I can't change image too often");
            }
            else
            {
                Debug.Log("Wow, these glasses look good on me");
                PlayerController.glasses = true;
                Global.PussyReactionToGlasses();
            }
        }
    }
}
