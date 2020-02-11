using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Vector3 respawnPoint;

    public void playerRespawn(){
        //currently resets player to one spawn point
        Transform playerTransform = GetComponentInParent<Transform>();
        playerTransform.position = respawnPoint;
    }
}
