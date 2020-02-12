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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            currentCharachterLocation = CharachterLocation.grounded;
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            currentCharachterLocation = CharachterLocation.onWall;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground")) {
            currentCharachterLocation = CharachterLocation.grounded;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {
            currentCharachterLocation = CharachterLocation.inAir;
        }
    }

}
