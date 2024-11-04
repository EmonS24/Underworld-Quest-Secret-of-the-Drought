using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushPull : MonoBehaviour
{
    private GameObject currentObject; // Objek yang sedang di-grab
    private PushableObject pushableObject; // Komponen PushableObject
    private Vector2 offset; // Offset posisi grab
    private PlayerVar player; // Mengacu pada PlayerVar

    void Start()
    {
        player = GetComponent<PlayerVar>(); // Ambil komponen PlayerVar dari player
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Menggunakan 'E' untuk grab
        {
            if (currentObject != null)
            {
                player.isGrabbing = !player.isGrabbing; // Toggle grab state
                pushableObject.SetKinematic(player.isGrabbing); // Set kinematic saat di-grab
            }
        }

        if (player.isGrabbing && currentObject != null)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        // Pindahkan objek berdasarkan posisi pemain
        currentObject.transform.position = (Vector2)transform.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable")) // Pastikan objek yang di-grab memiliki tag "Pushable"
        {
            currentObject = collision.gameObject;
            pushableObject = currentObject.GetComponent<PushableObject>(); // Ambil komponen PushableObject
            
            // Hitung offset agar objek berada di posisi yang tepat saat di-grab
            offset = (Vector2)(currentObject.transform.position - transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            currentObject = null; // Reset objek saat keluar dari trigger
            if (player.isGrabbing) // Jika sedang grab, lepas objek
            {
                player.isGrabbing = false;
                pushableObject.SetKinematic(false); // Kembalikan ke mode normal
            }
        }
    }
}
