using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 respawnPoint;
    public Image Skull1;
    public static int deathCount = 0;
    [SerializeField] private Text deathText;

    private Animator anim;

    public bool IsRespawn = false;

    private Rigidbody2D rb;

    void Start()
    {
        deathCount = 0;
        respawnPoint = transform.position;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Egel")
        {
            rb.gravityScale = 0;
            IsRespawn = true;
            anim.SetTrigger("death");
            GameObject.Find("player").GetComponent<New_Playermovement>().enabled = false; 
            
            
    
            
        }
        else if(collision.tag == "RespawnPoint")
        {
            respawnPoint = transform.position;
        }

        if(collision.tag == "Start")
        {
            IsRespawn = true;
        }

    }

    void respawn()
    {
        StartCoroutine(death());
    }


    IEnumerator death()
    {   
        deathCount++; //het zelfde als (Plankcount + 1)
        deathText.text = "x" + deathCount;
        transform.position = respawnPoint;
        rb.gravityScale = 2.7f;
        anim.SetBool("Run", false);
        anim.SetBool("Idle", false);
        anim.SetBool("Dash", false);
        anim.SetTrigger("respawn");
        yield return new WaitForSeconds(1.8f);
        GameObject.Find("player").GetComponent<New_Playermovement>().enabled = true;
        anim.SetBool("Idle", true);
        IsRespawn = false;

    }

}
