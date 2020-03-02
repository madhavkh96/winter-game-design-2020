using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Triggers : MonoBehaviour
{

    [Tooltip("Only Check one bool here according to the type of behaviour you want associated with it")]

    public bool m_Door;
    public Animator doorAnimator;
    [Header("------------------------------------------------")]
    public bool m_Tutorial;
    public GameObject tutorialText;

    private void OnTriggerEnter(Collider other)
    {
        if (m_Door)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                doorAnimator.SetTrigger("DoorOpen");
            }
        }


        if (m_Tutorial) {
            if (other.gameObject.CompareTag("Player")) {
                tutorialText.SetActive(true);
            }
        }
    }
}