using UnityEngine;

public class Global : MonoBehaviour
{
    private static GameObject[] NPC;
    private static Animator hintAnimator;
    private static string currentHint;
    public static void ChangeStatus(int val)
    {
        foreach (var pussy in NPC)
        {
            pussy.GetComponent<PussyScript>().ChangeStatus(val);
        }
    }
    public static void EndGame(){}

    public static void ChangeHintsState(string newHint)
    {
        //Stop animation from interrupting itself
        if (currentHint == newHint) return;
		
        //Play new animation
        hintAnimator.Play(newHint);

        //Update currentState
        currentHint = newHint;
    }

    public static void PussyReactionToGlasses()
    {
        foreach (var pussy in NPC)
        {
            pussy.GetComponent<PussyScript>().PlayerPutOnGlasses();
        }
    }
    
    private void Start()
    {
        hintAnimator = GameObject.FindGameObjectsWithTag("UI Hints")[0].GetComponent<Animator>();
        NPC = GameObject.FindGameObjectsWithTag("Pussy");
    }
}

