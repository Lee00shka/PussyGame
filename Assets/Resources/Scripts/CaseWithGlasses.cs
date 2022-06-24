using UnityEngine;

public class CaseWithGlasses : MonoBehaviour
{
    //Animation
    private const string GLASSES = "Glasses";
    private const string WITHOUT_GLASSES = "None";
    
    private bool flag = false;
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
    private void Update()
    {
        if (flag && Input.GetButtonDown("Fire1"))
        {
            if (PlayerController._glasses)
            {
                Debug.Log("I can't change image too often");
            }
            else
            {
                Debug.Log("Wow, these glasses look good on me");
                PlayerController.WearGlasses();
                Global.PussyReactionToGlasses();
            }
        }
    }
}
