using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    private float deathCount;
    private Vector3 respawnPoint;
    public Image Skull1;
    public Image Skull2;
    public Image Skull3;

    private Animator anim;

    public bool IsRespawn = false;
    public bool IsDeath = false;

    void Start()
    {
        Skull1.enabled = false;
        Skull2.enabled = false;
        Skull3.enabled = false;
        deathCount = 0f;
        respawnPoint = transform.position;
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(deathCount == 1)
        {
            Skull1.enabled = true;
        }
        if(deathCount == 2)
        {
            Skull2.enabled = true;
        }
        if(deathCount == 3)
        {
            Skull3.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Egel")
        {
            IsDeath = true;
            anim.SetTrigger("death");
            GameObject.Find("player").GetComponent<New_Playermovement>().enabled = false; 
            deathCount = deathCount + 1;
    
            
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
        IsDeath = false;
        IsRespawn = true;
        StartCoroutine(death());
    }


    IEnumerator death()
    {
        transform.position = respawnPoint;
        anim.SetBool("Run", false);
        anim.SetBool("Idle", false);
        anim.SetBool("Dash", false);
        anim.SetTrigger("respawn");
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("player").GetComponent<New_Playermovement>().enabled = true;
        anim.SetBool("Idle", true);
        IsRespawn = false;

    }

}
