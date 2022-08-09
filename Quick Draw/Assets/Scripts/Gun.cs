using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Cockpit;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int magazineMax;
    public int magazineCount;
    public int reloadTime;
    public int fireInterval;

    private bool _readyToFire;
    
    public GameObject bulletPrefab;
    public GameObject bulletController;// Reference to the bullet controller object
    
    private BulletController _bc;
    
    public Transform bulletSpawnPoint;
    
    public AudioSource audioSource;
    
    public AudioClip gunShot, falseFire; 
   
    private void Start()
    {
        bulletController = GameObject.Find("Bullet Controller");
        _bc = bulletController.GetComponent<BulletController>();
        //InvokeRepeating(nameof(Fire),3,0.5f);
    }

    /*
     * Fires a Bullet
     * If there are any inactive bullets, an inactive bullet is taken and removed from the inactive list
     * If there are no inactive bullets, a new bullet is instantiated
     */
    public void Fire()
    {
        if (!_readyToFire)
        {
            AudioSource.PlayClipAtPoint(falseFire,bulletSpawnPoint.position);
        }
        else
        {  
            GameObject bullet = _bc.GetBullet();

            var bulletPosition = bulletSpawnPoint.position;
            bullet.transform.position = bulletPosition;
            bullet.transform.rotation = bulletSpawnPoint.rotation;
        
            AudioSource.PlayClipAtPoint(gunShot,bulletPosition);
            _readyToFire = false;
        }
    }
   
}
