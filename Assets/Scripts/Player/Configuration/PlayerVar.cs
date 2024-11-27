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
    [SerializeField] GameObject pauseMenu;

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
}
}
