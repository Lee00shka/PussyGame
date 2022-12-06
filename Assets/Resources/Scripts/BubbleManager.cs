using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    public void CreateBubble(int prefabID, Transform obj)
    {
        Instantiate(prefabs[prefabID], obj.position, Quaternion.identity, obj);
    }
}