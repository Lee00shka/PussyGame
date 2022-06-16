using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public static Collider2D box;
    public static bool door = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pussy" && door)
        {
            //PussyScript.status = 2;
        }
    }

    private void Update()
    {
        box = GetComponent<Collider2D>();
    }
}
