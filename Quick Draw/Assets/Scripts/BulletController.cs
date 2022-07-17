using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public List<GameObject> inactiveBullets = new List<GameObject>();
    public List<GameObject> activeBullets = new List<GameObject>();

    private void Start()
    {
       //InvokeRepeating(nameof(Fire),3,5);
    }

    private void Update()
    {
        if(activeBullets.Count > 0) UpdateBullets();
    }

    /*
     * Fires a Bullet
     * If there are any inactive bullets, an inactive bullet is taken and removed from the inactive list
     * If there are no inactive bullets, a new bullet is instantiated
     */
    public void Fire()
    {
        GameObject bullet;
        if (inactiveBullets.Count > 0)
        {
            bullet = inactiveBullets[0];
            bullet.SetActive(true);
            bullet.transform.position = barrel.position;
            bullet.transform.rotation = barrel.rotation;
            inactiveBullets.Remove(bullet);
            activeBullets.Add(bullet);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
            activeBullets.Add(bullet);
        }
        
        bullet.GetComponent<Bullet>().active = true;
        AudioSource.PlayClipAtPoint(audioClip,barrel.position);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void UpdateBullets()
    {
        // ReSharper disable once ForCanBeConvertedToForeach
        // It cannot be converted to a foreach loop. The contents of activeBullets get changed so it would break
        for (var bulletIndex = 0; bulletIndex < activeBullets.Count; bulletIndex++)
        {
            var bullet = activeBullets[bulletIndex];
            bullet.GetComponent<Bullet>().UpdateLocation();
            if (!bullet.GetComponent<Bullet>().active)
            {
                PutAway(bullet);
            }
        }
    }

    // Resets bullet velocity and disables bullet
    private void PutAway(GameObject bullet)
    {
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        activeBullets.Remove(bullet);
        inactiveBullets.Add(bullet);
        bullet.SetActive(false);
    }
}