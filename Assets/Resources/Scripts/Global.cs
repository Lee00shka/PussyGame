using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    private static GameObject[] NPC;
    private static Animator hints;
    public static void ChangeStatus(int val)
    {
        foreach (var pussy in NPC)
        {
            pussy.GetComponent<PussyScript>().ChangeStatus(val);
        }
    }
    public static void EndGame(){}
    
    private void Start()
    {
        hints = GameObject.FindGameObjectsWithTag("UI Hints")[0].GetComponent<Animator>();
        NPC = GameObject.FindGameObjectsWithTag("Pussy");
    }
}
