using UnityEngine;

public class BubbleLine : MonoBehaviour
{
    private float lifeTime = 2f;

    private void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
