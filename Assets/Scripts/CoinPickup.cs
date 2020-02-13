using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
	public AudioSource coinSource;
    public AudioClip coinPickup;
	public float rotationSpeed = 90f;
	Transform coin;
    // Start is called before the first frame update
    void Start()
    {
        coin = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        coin.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }

	private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
			coinSource.PlayOneShot(coinPickup, 1.0f);
            other.gameObject.GetComponent<HUDscoreTracker>().addScore(1);
			Destroy(gameObject, .4f);
        }
    }
}
