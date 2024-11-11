using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Animator animator;
    private EnemyVar enemy;


    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<EnemyVar>();
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
