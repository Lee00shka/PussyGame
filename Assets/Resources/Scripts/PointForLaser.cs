using UnityEngine;

public class PointForLaser : MonoBehaviour
{
    private float lifeTime = 10;

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
