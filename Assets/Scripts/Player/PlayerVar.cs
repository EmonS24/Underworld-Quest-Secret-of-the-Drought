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
}
