using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    private static GameObject[] NPC;
    private static GameObject player;
    public static int numOfPussies;
    public static int numOfEnchanted = 0;
    private static Animator hintAnimator;
    private static string currentHint;
    public TextMeshProUGUI charmed;

    //Global ivents
    public static void GameOver()
    {
        EndGame.End("Lose");
    }

    public static void WinGame()
    {
        if (numOfPussies == numOfEnchanted)
        {
            Debug.Log("We enchanted!");
            EndGame.End("Win");
        }
    }
    public static void SpawnBubbleLine()
    {
        
    }
    //Changing every pussy
    public static void ChangeStatus(int val)
    {
        foreach (var pussy in NPC)
        {
            pussy.GetComponent<PussyScript>().ChangeStatus(val);
        }
    }
    public static void PussyReactionToGlasses()
    {
        foreach (var pussy in NPC)
        {
            pussy.GetComponent<PussyScript>().PlayerPutOnGlasses();
        }
    }
    //UI
    public void RenderText()
    {
        charmed.text = "CHARMED: " + numOfEnchanted + "/" + numOfPussies;
    }
    
    //Animation
    public static void ChangeHintsState(string newHint)
    {
        //Stop animation from interrupting itself
        if (currentHint == newHint) return;
		
        //Play new animation
        hintAnimator.Play(newHint);

        //Update currentState
        currentHint = newHint;
    }
    public static void ChangeLayer()
    {
        foreach (var pussy in NPC)
        {
            int y = (int)Math.Round(pussy.transform.position.y * 10);
            pussy.GetComponent<SpriteRenderer>().sortingOrder = -y;
        }
        int yPlayer = (int)Math.Round(player.transform.position.y * 10);
        player.GetComponent<SpriteRenderer>().sortingOrder = -yPlayer;
    }
    //Standart
    private void Start()
    {
        hintAnimator = GameObject.FindGameObjectsWithTag("UI Hints")[0].GetComponent<Animator>();
        NPC = GameObject.FindGameObjectsWithTag("Pussy");
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        numOfPussies = NPC.Length;
        RenderText();
    }

    private void Update()
    {
        RenderText();
        ChangeLayer();
    }
}

