using UnityEngine;

public class InstantDestruction : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        GameManager.instance.MissNote();
    }
}
