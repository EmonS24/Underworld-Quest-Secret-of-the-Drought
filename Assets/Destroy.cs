using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Destroy(collision.gameObject);
    }
}
