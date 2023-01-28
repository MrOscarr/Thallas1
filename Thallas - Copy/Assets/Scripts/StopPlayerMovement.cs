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
        GameObject.Find("player").GetComponent<New_Playermovement>().enabled = false;
        yield return new WaitForSeconds(5.5f);
        GameObject.Find("player").GetComponent<New_Playermovement>().enabled = true; 
    }
        

}
