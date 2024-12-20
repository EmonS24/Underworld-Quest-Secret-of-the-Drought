using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private PlayerVar player;
    private PlayerMov move;


    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerVar>();
        move = GetComponent<PlayerMov>();
    }

    void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("speed", move.GetCurrentSpeed());
        animator.SetBool("isMove", player.isMove);
        animator.SetBool("isGrounded", player.isGrounded);
        animator.SetBool("isClimbing", player.isClimbing);
        animator.SetBool("isCrouching", player.isCrouching);
        animator.SetBool("isGrabbing", player.isGrabbing);
        animator.SetBool("isDeath", player.isDeath);
    }
}
