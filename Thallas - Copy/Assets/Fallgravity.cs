using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallgravity : MonoBehaviour
{

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "zero")
        {
            rb.gravityScale = 0.3f;
        }

        if(collision.tag == "one")
        {
            rb.gravityScale = 2.7f;
        }
    }
}
