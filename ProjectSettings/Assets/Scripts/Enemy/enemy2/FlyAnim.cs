using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAnim : MonoBehaviour
{
    private Animator animator;
    private FlyFollow fly;


    void Start()
    {
        animator = GetComponent<Animator>();
        fly = GetComponent<FlyFollow>();
    }

    void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        animator.SetBool("isChasing", fly.isChasing);
    }
}
