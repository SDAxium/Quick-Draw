using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    void Start()
    {
        
    }
    
    void Update()
    {
        
        
    }
    
    void MEHFIRE()
    {
        GameObject gun = GameObject.Find("GunParent");
        gun.GetComponent<GunScript>().Fire();
    }
}
