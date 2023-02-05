using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{

    public GameObject Endscene;
    public GameObject plankcount;
    public GameObject plank;
    public GameObject skullcount;
    public GameObject skull;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "End")
        {
            plankcount.SetActive(false);
            plank.SetActive(false);
            skullcount.SetActive(false);
            skull.SetActive(false);
            Endscene.SetActive(true);
        }
    }
}
