using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int plankCount;
    public float[] position;

    public PlayerData (PlankCollector player)
    {
        plankCount = PlankCollector.PlankCount;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.x;
    }
}
