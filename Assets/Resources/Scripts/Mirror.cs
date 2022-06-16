using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetButton("Fire1"))
            {
                Debug.Log("Mirror");
                PlayerController.UseMirror();
            }
        }
    }
}
