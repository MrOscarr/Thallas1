using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStopMovement : MonoBehaviour
{
    private PlayerDeath playerDeath;


    void Awake()
    {
        playerDeath = GameObject.Find("player").GetComponent<PlayerDeath>();
    }

    void OnEnable()
    {
        playerDeath.IsRespawn = false;
    }
}
