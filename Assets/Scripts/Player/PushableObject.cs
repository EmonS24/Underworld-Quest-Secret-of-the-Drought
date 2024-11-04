using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetKinematic(bool isKinematic)
    {
        rb.isKinematic = isKinematic; // Atur status kinematic untuk objek ini
    }
}
