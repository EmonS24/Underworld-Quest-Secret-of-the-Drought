using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAnim : MonoBehaviour
{
    private Animator animator;
    private RatVar enemy;


    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<RatVar>();
    }

    void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        animator.SetBool("isMove", enemy.isMove);
        animator.SetBool("isAttack", enemy.isAttack);
    }
}
