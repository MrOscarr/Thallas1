using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlankCollector : MonoBehaviour
{
    public static int PlankCount = 0;

    [SerializeField] private Text PlankText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Plank"))
        {
            Destroy(collision.gameObject);
            PlankCount++; //het zelfde als (Plankcount + 1)
            Debug.Log(" "+ PlankCount);
            PlankText.text = " " + PlankCount;
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        PlankCount = data.plankCount;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;


    }
}
