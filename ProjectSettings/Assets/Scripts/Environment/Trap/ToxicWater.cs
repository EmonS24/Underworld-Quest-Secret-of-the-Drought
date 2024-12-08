using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicWater : MonoBehaviour
{
    private PlayerVar player;

    private void Start()
    {
        player = FindObjectOfType<PlayerVar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.isDeath = true; 
        }
    }
}
