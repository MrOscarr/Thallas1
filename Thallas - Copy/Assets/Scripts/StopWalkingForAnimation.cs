using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWalkingForAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            rb.velocity = Vector2.zero;
        }
    }
}

