using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalPlatformMovement : MonoBehaviour
{
    public float speed = 0.5f;
    public float movementLength = 2.0f;
    Transform platform;
    private float maxHeight;
    private float minHeight;
    private bool ascending = true;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Transform>();
        maxHeight = platform.position.y + movementLength;
        minHeight = platform.position.y - movementLength;
    }

    // Update is called once per frame
    void Update()
    {
        if(ascending && platform.position.y < maxHeight){
            platform.position += new Vector3(0, speed * Time.deltaTime, 0);
            if(platform.position.y > maxHeight){
                ascending = false;
                platform.position = new Vector3(platform.position.x, maxHeight, platform.position.z);
            }
        }
        else if(!ascending && platform.position.y > minHeight){
            platform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            if(platform.position.y < minHeight){
                ascending = true;
                platform.position = new Vector3(platform.position.x, minHeight, platform.position.z);
            }
        }
    }
}
