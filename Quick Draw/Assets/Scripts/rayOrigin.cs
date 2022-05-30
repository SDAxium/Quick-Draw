using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayOrigin : MonoBehaviour
{
    public Transform parentTransform;
    void Update()
    {
        gameObject.transform.position = parentTransform.position;
    }
}
