using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private PlayerMov playerMov;
    private PlayerCrouch playerCrouch;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMov = GetComponent<PlayerMov>();
        playerCrouch = GetComponent<PlayerCrouch>(); 
    }

    void Update()
    {
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        PlayerVar.isMove = playerMov.IsMoving();
        animator.SetBool("isMove", PlayerVar.isMove);

        animator.SetFloat("speed", playerMov.GetCurrentSpeed());

        animator.SetBool("isGrounded", PlayerVar.isGrounded);

        animator.SetBool("isCrouching", playerCrouch.isCrouching); 

        animator.SetBool("isClimbing", PlayerVar.isClimbing);
    }
}
