using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    public void CreateBubble(int prefabID, Vector2 position)
    {
        Instantiate(prefabs[prefabID], position, Quaternion.identity);
    }
}
