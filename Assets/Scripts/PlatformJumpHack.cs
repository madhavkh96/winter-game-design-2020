using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformJumpHack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other.gameObject.GetComponent<Rigidbody>().velocity.y > 0);

            if (other.gameObject.GetComponent<Rigidbody>().velocity.y > 0)
            {
                Physics.IgnoreCollision(this.gameObject.transform.parent.transform.parent.GetComponent<BoxCollider>(), other.gameObject.GetComponent<BoxCollider>(), true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            Physics.IgnoreCollision(this.gameObject.transform.parent.transform.parent.GetComponent<BoxCollider>(), other.gameObject.GetComponent<BoxCollider>(), false);
        }
    }
}
