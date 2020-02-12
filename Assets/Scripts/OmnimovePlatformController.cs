using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmnimovePlatformController : MonoBehaviour
{
    public float speed = 0.5f;
    Transform platform;
    public Vector3 Endpoint1;
    public Vector3 Endpoint2;

    private bool towardsEndpoint1 = true;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if(towardsEndpoint1){
            var heading = Endpoint1 - platform.position;
            if (heading.magnitude < speed){
                //if we will overshoot, do nothing and set to head back to the other point
                towardsEndpoint1 = false;
            }
            else{
                var direction = heading / heading.magnitude;
                platform.position += direction * speed * Time.deltaTime;
            }
        }
        else{
            var heading = Endpoint2 - platform.position;
            if (heading.magnitude < speed){
                //if we will overshoot, do nothing and set to head back to the other point
                towardsEndpoint1 = true;
            }
            else{
                var direction = heading / heading.magnitude;
                platform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
