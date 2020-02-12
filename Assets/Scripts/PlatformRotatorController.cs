using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotatorController : MonoBehaviour
{
    public float rotationSpeed = 90f;
    //NOTE: Only allows one rotation at a time
    public bool rotateX = true;
    public bool rotateZ = false;
    public bool rotateY = false;
    Transform platform;
    private bool rotating = true;
    public float timeBetweenRotate = 5.0f;
    private float rotateTimer;
    private float waitTimer;
    public float timeToRotate = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Transform>();
        waitTimer = timeBetweenRotate;
        rotateTimer = timeToRotate;
    }

    // Update is called once per frame
    void Update()
    {
        if(rotating){
            rotateTimer -= Time.deltaTime;
            if(rotateTimer <= 0){
                if(rotateX)
                    platform.Rotate(rotationSpeed * (Time.deltaTime + rotateTimer), 0, 0, Space.Self);
                else if(rotateY)
                    platform.Rotate(0, rotationSpeed * (Time.deltaTime + rotateTimer), 0, Space.Self);
                else if(rotateZ)
                    platform.Rotate(0, 0, rotationSpeed * (Time.deltaTime + rotateTimer), Space.Self);
                rotating = false;
                rotateTimer = timeToRotate;
            }
            else{
                if(rotateX)
                    platform.Rotate(rotationSpeed * Time.deltaTime, 0, 0, Space.Self);
                else if(rotateY)
                    platform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
                else if(rotateZ)
                    platform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
            }
            
        }
        else{
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0){
                rotating = true;
                waitTimer = timeBetweenRotate;
            }
        }
    }
}
