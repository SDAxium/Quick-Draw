using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    void Start()
    {
       // InvokeRepeating(nameof(MehFire),3f,10f);
    }
    
    void Update()
    {
        
        
    }
    
    /*void MehFire()
    {
        GameObject target = GameObject.Find("Target Object");
        target.GetComponent<TargetActivator>().Activate();
        Invoke(nameof(MehFirent),2f);
    }

    void MehFirent()
    {
        GameObject target = GameObject.Find("Target Object");
        target.GetComponent<TargetActivator>().Deactivate();
    }*/
}
