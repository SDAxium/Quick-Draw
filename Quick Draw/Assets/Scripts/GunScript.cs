using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float speed = 40;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;
    
    public void Fire()
    {
        Vector3 distance = new Vector3(0, 0, 0.16f);
        GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
        audioSource.PlayOneShot(audioClip);
        Destroy(spawnedBullet, 2f);
    }
}