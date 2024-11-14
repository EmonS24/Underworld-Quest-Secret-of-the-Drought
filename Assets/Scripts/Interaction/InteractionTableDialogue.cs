using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionTableDialogue : MonoBehaviour
{
    [SerializeField] private GameObject ui;

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(message:"Player dalam interaksi!");
        ui.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(message:"Object Terdeteksi");
        ui.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ui.SetActive(false);
    }
}
