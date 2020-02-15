using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformJumpHack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            Physics.IgnoreCollision(this.gameObject.transform.parent.GetComponent<BoxCollider>(), other.gameObject.GetComponent<BoxCollider>(), true);
            //other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, gameObject.transform.parent.position.y + 0.5f, other.gameObject.transform.position.z); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            Physics.IgnoreCollision(this.gameObject.transform.parent.GetComponent<BoxCollider>(), other.gameObject.GetComponent<BoxCollider>(), false);
        }
    }
}
