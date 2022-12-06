using UnityEngine;

public class Laser : MonoBehaviour
{
    private bool flag = false;
    [SerializeField] private GameObject prefab;
    private Transform pointForLaser;
    
    private const string DISCO = "Disco";
    private const string WITHOUT_DISCO = "None";
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            flag = true;
            Global.ChangeHintsState(DISCO);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            flag = false;
            Global.ChangeHintsState(WITHOUT_DISCO);
        }
    }

    private void Start()
    {
        pointForLaser = transform.GetChild(0);
    }

    private void Update()
    {
        if (flag && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(prefab, pointForLaser.position, Quaternion.identity, pointForLaser);
            Debug.Log("The laser is activated");
            Global.ChangeStatus(1);
        }
    }
}
