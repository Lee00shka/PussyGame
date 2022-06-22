using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    private static GameObject[] NPC;
    public static void ChangeStatus(int val)
    {
        foreach (var pussy in NPC)
        {
            pussy.GetComponent<PussyScript>().ChangeStatus(val);
        }
    }

    private void Start()
    {
        NPC = GameObject.FindGameObjectsWithTag("Pussy");
    }
}
