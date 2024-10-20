using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVar : MonoBehaviour
{
    public static bool isMove;     
    public static bool isJumping; 
    public static bool isFalling;    
    public static bool isGrabbing;   
    public static bool isGrounded;   
    public static bool isCrouching;   
    public static bool isLedgeGrabbing;   
    public static bool isClimbing; 
    public static LayerMask groundLayer;   

    private void Update()
    {
        
    }
}
