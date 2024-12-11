using UnityEngine;

public class LeafhopperAnim : MonoBehaviour
{
    private Animator animator;
    private LeafhopperVar Leafhopper;


    void Start()
    {
        animator = GetComponent<Animator>();
        Leafhopper = GetComponent<LeafhopperVar>();
    }

    void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        animator.SetBool("isMove", Leafhopper.isMove);
        animator.SetBool("isAttack", Leafhopper.isAttack);
    }
}
