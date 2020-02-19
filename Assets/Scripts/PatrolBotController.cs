using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBotController : MonoBehaviour
{
    public GameObject laserPrefab;
    public float speed = 0.5f;
    public float engagementRange = 15f;
    private Transform patrolBot;
    public Vector3 Endpoint1;
    public Vector3 Endpoint2;
    
    public AudioSource botSource;
    public AudioClip laserFire;

    private bool towardsEndpoint1 = true;

    public float laserFireTime = 5f;
    public float laserCD;
    
    private GameObject player;
    public bool laserReady = true;

    // Start is called before the first frame update
    void Start()
    {
        patrolBot = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        laserCD = laserFireTime;
    }

    // Update is called once per frame
    void Update()
    {   
        if (!laserReady)
        {
            laserFireTime -= Time.deltaTime;
            if (laserFireTime <= 0)
            {
                laserReady = true;
                laserFireTime = laserCD;
            }
        }
        Vector3 heading = player.GetComponent<Transform>().position - patrolBot.position;
        //Debug.Log(heading.magnitude);
        if (heading.magnitude <= engagementRange)
        {
            patrolBot.rotation = Quaternion.LookRotation(heading, Vector3.up);
            exterminationProtocol(heading);
        }
        movementController();
    }

    private void exterminationProtocol(Vector3 heading)
    {
        if (laserReady)
        {
            GameObject laser = Instantiate(laserPrefab,
                // Place it 0.5 units in front
                transform.TransformPoint(new Vector3(0.5f, 0, 0)),
                // Point it at player
                transform.rotation);
            botSource.PlayOneShot(laserFire, 1.0f);
            laserReady = false;
        }
    } 

    private void movementController()
    {
        if(towardsEndpoint1){
            var heading = Endpoint1 - patrolBot.position;
            if (heading.magnitude < speed){
                //if we will overshoot, do nothing and set to head back to the other point
                towardsEndpoint1 = false;
            }
            else{
                var direction = heading / heading.magnitude;
                patrolBot.position += direction * speed * Time.deltaTime;
            }
        }
        else{
            var heading = Endpoint2 - patrolBot.position;
            if (heading.magnitude < speed){
                //if we will overshoot, do nothing and set to head back to the other point
                towardsEndpoint1 = true;
            }
            else{
                var direction = heading / heading.magnitude;
                patrolBot.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
