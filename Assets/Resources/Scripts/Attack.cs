using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //Animation
    private const string CHARM = "Charm";
    private const string WITHOUT_HINTS = "None";
    
    //Just
    private Dictionary<string, Collider2D> inTrigger;
    private int flag = 0;
    
    //Event Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pussy")
        {
            inTrigger.Add(other.name, other);
            Global.ChangeHintsState(CHARM);
            flag += 1;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pussy")
        {
            inTrigger.Remove(other.name);
            flag -= 1;
            if (flag == 0)
            {
                Global.ChangeHintsState(WITHOUT_HINTS); 
            }
        }
    }
    
    //Mechanic
    private void Charm()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (flag > 0)
            {
                Collider2D minPussy = null;
                float min = 1000000;
                float count;
                foreach (var pussy in inTrigger)
                {
                    count = Vector2.Distance(pussy.Value.transform.position, transform.position );
                    if (count < min)
                    {
                        min = count;
                        minPussy = pussy.Value;
                    }
                }
                Global.ChangeStatus(3);
                minPussy.gameObject.GetComponent<PussyScript>().Mark();
                Debug.Log("Have fun, " + minPussy.gameObject.name);
            } 
        }
    }
    
    //Standart
    private void Start()
    {
        inTrigger = new Dictionary<string, Collider2D>(); 
    }
    private void Update()
    {
        Charm();
    }
}
