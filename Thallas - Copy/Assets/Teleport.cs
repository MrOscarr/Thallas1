using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Portal;
    public GameObject player;
    public GameObject BlackScreen;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(Teleport1());
        }
    }

    IEnumerator Teleport1()
    {
        yield return new WaitForSeconds(0.5f);
        BlackScreen.SetActive(true);
        player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
        yield return new WaitForSeconds(0.5f);
        BlackScreen.SetActive(false);


    }
}
