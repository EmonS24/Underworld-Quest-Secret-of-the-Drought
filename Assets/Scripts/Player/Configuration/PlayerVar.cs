using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVar : MonoBehaviour
{
    public bool isMove;     
    public bool isJumping; 
    public bool isGrounded;   
    public bool isClimbing;  
    public bool canClimb;  
    public bool isCrouching; 
    public bool isGrabbing = false; 
    public bool isPushing; 
    public bool isPulling; 
    public bool isDeath = false;
    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject questMenu; 

    private void Start() 
    {
        Time.timeScale = 1;
        isPaused = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pauseMenu.SetActive(true);
                isPaused = true;
                Time.timeScale = 0;
            }
            else if (isPaused)
            {
                pauseMenu.SetActive(false);
                isPaused = false;
                Time.timeScale = 1;
            }
        } 
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isPaused)
            {
                QuestPause();
            }
            else if (isPaused)
            {
                QuestBack();
            }
        } 
    }

    public void QuestPause()
    {
        questMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void QuestBack()
    {
        questMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
}
