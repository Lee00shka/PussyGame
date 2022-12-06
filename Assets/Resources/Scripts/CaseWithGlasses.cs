using UnityEngine;

public class CaseWithGlasses : MonoBehaviour
{
    //Animation
    private const string GLASSES = "Glasses";
    private const string WITHOUT_GLASSES = "None";
    
    private bool flag = false;
    
    //Bubble
    private Transform pointForBuuble;
    private BubbleManager bubbleManager;
    
    //Event Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            flag = true;
            Global.ChangeHintsState(GLASSES);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            flag = false;
            Global.ChangeHintsState(WITHOUT_GLASSES);
        }
    }
    //Standart
    private void Start()
    {
        pointForBuuble = GameObject.FindGameObjectsWithTag("Player")[0].transform.GetChild(0);
        bubbleManager = GameObject.Find("BubbleManager").GetComponent<BubbleManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && flag)
        {
            if (PlayerController._glasses)
            {
                bubbleManager.CreateBubble(1, pointForBuuble);
                Debug.Log("I can't change image too often");
            }
            else
            {
                bubbleManager.CreateBubble(0, pointForBuuble);
                Debug.Log("Wow, these glasses look good on me");
                PlayerController.WearGlasses();
                Global.PussyReactionToGlasses();
            }
        }
    }
}
