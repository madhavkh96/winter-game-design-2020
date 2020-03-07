using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 1000f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource audioSrc;

    private float nextTimeToFire = 0f;

    void Start ()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float primaryAttack = Input.GetAxis("Fire1");
        if (primaryAttack > 0 && Time.time >= nextTimeToFire && GameManager.instance.canShoot)
        {
            nextTimeToFire = Time.time + (1f / fireRate);
            Shoot();
        }

    }

    void Shoot ()
    {
        muzzleFlash.Play();
        audioSrc.Play();
        Debug.Log("Shoot");
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                Debug.Log("Target Taking Damage");
                target.TakeDamage(damage);
            }
            
            if (hit.rigidbody != null)
            {
                Debug.Log("Not A Target");
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

        //StartCoroutine(Recoil());
    }
        
    IEnumerator Recoil() {
        Vector3 finalTransform = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y + 10f, Camera.main.transform.eulerAngles.z);
        Vector3 initialTransform = Camera.main.transform.eulerAngles;
        
        while (Camera.main.transform.eulerAngles.y < finalTransform.y) {
            Camera.main.transform.eulerAngles = Vector3.Lerp(Camera.main.transform.eulerAngles, finalTransform, Time.deltaTime * 10);
        }
        
        yield return new WaitForSeconds(0.3f);
        
        while (Camera.main.transform.eulerAngles.y > initialTransform.y) {
            Camera.main.transform.eulerAngles = Vector3.Lerp(Camera.main.transform.eulerAngles, initialTransform, Time.deltaTime * 10);
        }
    }
}
