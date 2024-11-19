using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    private float speed;  
    private float endPointX;  

    public void Fly(float speed, float endPointX)
    {
        this.speed = speed;  
        this.endPointX = endPointX; 
    }

    void Update()
    {
        if (transform.position.x < endPointX)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
