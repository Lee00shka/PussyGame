using UnityEngine;

public class Global : MonoBehaviour
{
    private static GameObject[] NPC;
    //public static int numOfPussies;
    //public static int numOfEnchanted = 0;
    
    private static Animator hintAnimator;
    private static string currentHint;
    
    //Global ivents
    public static void EndGame(){}
    
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
    
    //Standart
    private void Start()
    {
        hintAnimator = GameObject.FindGameObjectsWithTag("UI Hints")[0].GetComponent<Animator>();
        NPC = GameObject.FindGameObjectsWithTag("Pussy");
        ScoreManager.numOfPussies = NPC.Length;
    }
}

