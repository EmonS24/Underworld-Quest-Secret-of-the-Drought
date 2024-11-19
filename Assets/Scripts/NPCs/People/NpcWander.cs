using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Tilemaps;
using UnityEngine;

public class NpcWander : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float leftPatrolX, rightPatrolX;
    [SerializeField] private int facingDirection = -1;
    [SerializeField] private float minPauseTime, maxPauseTime;
    [SerializeField] private float minWalkTime, maxWalkTime;
    private bool isWalking;
    private float randomTime, timer;
    private bool isFlipping;

    private void Start()
    {
        randomTime = Random.Range(minWalkTime, maxWalkTime);
        anim.SetBool("isWalking", isWalking ? true : false);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= randomTime)
            StateChange();
        if (!isFlipping && (transform.position.x > rightPatrolX || transform.position.x < leftPatrolX))
            StartCoroutine(Flip());

        if (isWalking)
        {
            rb.velocity = Vector2.right * facingDirection * speed;
        }
        else
        {
            rb.velocity = Vector2.zero; 
        }
    }

    IEnumerator Flip()
    {
        isFlipping = true;
        transform.Rotate(0, 180, 0);
        facingDirection *= -1;
        yield return new WaitForSeconds(0.5f);
        isFlipping = false;
    }

    void StateChange()
    {
        isWalking = !isWalking;
        anim.SetBool("isWalking", isWalking ? true : false);
        randomTime = isWalking ? Random.Range(minWalkTime, maxWalkTime) : Random.Range(minPauseTime, maxPauseTime);
        timer = 0;
    }
}
