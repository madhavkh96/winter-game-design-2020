using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterLocState : MonoBehaviour
{
    public enum CharachterLocation { 
    grounded,
    inAir,
    onWall
    }

    public CharachterLocation currentCharachterLocation;

    public static CharachterLocState instance = null;

    private void Awake()
    {
        //Creating Singleton
        if (instance == null)
            instance = this;
        else if (instance != null) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            currentCharachterLocation = CharachterLocation.grounded;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            currentCharachterLocation = CharachterLocation.onWall;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("")) {
            currentCharachterLocation = CharachterLocation.inAir;
        }
    }

}
