using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Photon.Pun.Demo.Cockpit;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public InputActionReference reloadInputActionReferenceLeft,reloadInputActionReferenceRight;

    public TextMeshProUGUI bulletCount;
      
    public int magazineMax;
    public int magazineCount;
    public float reloadTime;
    public float fireInterval;

    private bool _readyToFire = true;
      
    public GameObject bulletPrefab;
    public GameObject bulletController;// Reference to the bullet controller object
    
    private BulletController _bc;
    
    public Transform bulletSpawnPoint;
    
    public AudioSource audioSource;
    
    public AudioClip gunShot, falseFire, chamberEmpty, reloadClip; 
   
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bulletController = GameObject.Find("Bullet Controller");
        _bc = bulletController.GetComponent<BulletController>();
        magazineCount = 20;
        bulletCount.text = magazineCount.ToString();
        //InvokeRepeating(nameof(Fire),3,0.5f);
    }

    private void Update()
    {
        reloadInputActionReferenceLeft.action.started += Reload;
        reloadInputActionReferenceRight.action.started += Reload;
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
            // if gun can fire
            //  check if the gun has any bullets remaining
            // if it does, get a bullet from the bullet controller
            // fire said bullet
            // if not, make player reload
            if (magazineCount > 0)
            {
                GameObject bullet = _bc.GetBullet();

                var bulletPosition = bulletSpawnPoint.position;
                bullet.transform.position = bulletPosition;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
        
                AudioSource.PlayClipAtPoint(gunShot,bulletPosition);
                _readyToFire = false;
                StartCoroutine(ReEnableFire());
                magazineCount--;
                bulletCount.text = magazineCount.ToString();
            }
            else
            {
                AudioSource.PlayClipAtPoint(chamberEmpty,bulletSpawnPoint.position);
            }
           
        }
    }

    public void Reload(InputAction.CallbackContext context)
    {
        StartCoroutine(ReloadEnumerator());
    }

    public IEnumerator ReloadEnumerator()
    {
        if (magazineCount >= magazineMax) yield break;
        AudioSource.PlayClipAtPoint(reloadClip,bulletSpawnPoint.position);
        yield return new WaitForSeconds(reloadTime);
        magazineCount = magazineMax;
        bulletCount.text = magazineCount.ToString();
    }

    public IEnumerator ReEnableFire()
    {
        yield return new WaitForSeconds(fireInterval);
        _readyToFire = true;
    }
}
