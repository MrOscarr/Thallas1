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

    // Start is called before the first frame update
    void Start()
    {
        Skull1.enabled = false;
        Skull2.enabled = false;
        Skull3.enabled = false;
        deathCount = 0f;
        respawnPoint = transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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
            anim.SetTrigger("death");  
            deathCount = deathCount + 1; 
            transform.position = respawnPoint;
            anim.SetTrigger("respawn");
            //StartCoroutine(death());
        }
        else if(collision.tag == "RespawnPoint")
        {
            respawnPoint = transform.position;
        }
    }

    //IEnumerator death()
    //{
        //anim.SetTrigger("death");
        //GameObject.Find("Player").GetComponent<New_Playermovement>().enabled = false;
       // yield return new WaitForSeconds(3.0f);
        //transform.position = respawnPoint;
        //anim.SetTrigger("respawn");
        //yield return new WaitForSeconds(3.0f);
        //GameObject.Find("Player").GetComponent<New_Playermovement>().enabled = true; 
        //deathCount = deathCount + 1; 
    //}
}
