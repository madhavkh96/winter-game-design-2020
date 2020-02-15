using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocState : MonoBehaviour
{
    public enum CharacterLocation { 
    grounded,
    inAir,
    onWall
    }

    public CharacterLocation currentCharacterLocation;

    public static CharacterLocState instance = null;

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
            currentCharacterLocation = CharacterLocation.grounded;
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            currentCharacterLocation = CharacterLocation.onWall;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground")) {
            currentCharacterLocation = CharacterLocation.grounded;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {
            currentCharacterLocation = CharacterLocation.inAir;
        }
    }

}
