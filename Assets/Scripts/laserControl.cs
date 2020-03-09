using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    public float laserSpeed = 5f;
    public float laserLife = 5f;

    public float damage = 10f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //rotate laser to point at player
        transform.rotation *= Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        laserLife -= Time.deltaTime;
        if (laserLife <= 0)
        {
            Destroy(gameObject);
        }
        //uses up because forward is with the side of the capsule shape
        transform.position += transform.up * laserSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Respawner>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
