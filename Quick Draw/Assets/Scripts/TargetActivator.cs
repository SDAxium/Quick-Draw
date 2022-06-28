using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetActivator : MonoBehaviour
{
    public AudioClip blindPing;
    public AudioClip spawnSound;

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        gameObject.SetActive(false);
        Invoke(nameof(turnBackOn),5f);
    }

    void turnBackOn()
    {
        gameObject.SetActive(true);
    }
   
}
