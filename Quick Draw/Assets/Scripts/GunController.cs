using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float speed = 40;
    public GameObject bulletPrefab;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;

    List<GameObject> _inactiveBullets;
    private List<GameObject> _activeBullets;

    private void Awake()
    {
        _inactiveBullets = new List<GameObject>();
        _activeBullets = new List<GameObject>();
    }

    private void Update()
    {
        if(_activeBullets.Count > 0) UpdateBullets();
    }

    public void Fire()
    {
        GameObject bullet;
        if (_inactiveBullets.Count > 0)
        {
            bullet = _inactiveBullets[0];
            bullet.SetActive(true);
            bullet.transform.position = barrel.position;
            bullet.transform.rotation = barrel.rotation;
            _inactiveBullets.Remove(bullet);
            _activeBullets.Add(bullet);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
            _activeBullets.Add(bullet);
        }
        // Destroy(bullet, 2f);
       
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void UpdateBullets() 
    {
        foreach (var bullet in _activeBullets.Where(bullet => bullet.GetComponent<Rigidbody>().velocity.Equals(Vector3.zero)))
        {
            bullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
            AudioSource.PlayClipAtPoint(audioClip,barrel.position);
            StartCoroutine(PutAway(5f, bullet));
        }
    }

    // After a certain amount of time passes, disables bullet 
    private IEnumerator PutAway(float waitTime, GameObject bullet)
    {
        yield return new WaitForSeconds(waitTime);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _activeBullets.Remove(bullet);
        _inactiveBullets.Add(bullet);
        bullet.SetActive(false);
    }
}