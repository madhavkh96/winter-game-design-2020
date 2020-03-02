using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public string deathMessage;


    private void Start()
    {
        if (deathMessage == null) {
            deathMessage = "Falling";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Respawner>().playerRespawn(deathMessage);
        }
    }

}
