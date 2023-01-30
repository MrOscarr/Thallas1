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

    void Start()
    {
        deathCount = 0;
        respawnPoint = transform.position;
        anim = GetComponent<Animator>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Egel")
        {
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
        deathCount++; //het zelfde als (Plankcount + 1)
        deathText.text = "x" + deathCount;
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
