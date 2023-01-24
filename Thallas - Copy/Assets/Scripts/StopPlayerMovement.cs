using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait()); 
    }

    IEnumerator Wait()
    {
        GameObject.Find("Player").GetComponent<New_Playermovement>().enabled = false;
        yield return new WaitForSeconds(6.7f);
        GameObject.Find("Player").GetComponent<New_Playermovement>().enabled = true; 
    }
        

}
