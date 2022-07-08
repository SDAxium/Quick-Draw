using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetActivator : MonoBehaviour
{
    void Start()
    {
        
    }

    /*
     * On collision with another object, deactivate current game object for 5 seconds
     */
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            gameObject.SetActive(false);    
        }
    }
}
