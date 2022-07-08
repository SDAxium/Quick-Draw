using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletTarget : MonoBehaviour
{
    public AudioClip blindPing;
    public AudioClip spawnSound;

    public GameObject player;
    private float _targetSpeed;

    private float _rotationHeight;
    private float _rotationWidth;
    
    private float _timer;

    public float _rotationCenterX;
    public float _rotationCenterY;
    public float _rotationCenterZ;

    private bool _oscillationIsHorizontal;

    public void SetNewRandomValues()
    {
        _timer = Random.Range(0, 180);
        _targetSpeed = Random.Range(1, 5);
        _rotationHeight = Random.Range(3, 15);
        _rotationWidth = Random.Range(3, 15);
        _oscillationIsHorizontal = Random.value >= 0.5;
        SetRotationCenter(player.transform);
    }
    public void UpdateLocation()
    {
        Debug.Log("Updating Location");
        _timer += Time.deltaTime*_targetSpeed;
        var horizontalRotation = Mathf.Cos(_timer) * _rotationWidth + _rotationCenterX;
        var verticalRotation = Mathf.Sin(_timer) * _rotationHeight + _rotationCenterY;

        transform.position = new Vector3(horizontalRotation, verticalRotation, _rotationCenterZ);
        Debug.Log("Location: " + transform.position);
        transform.LookAt(player.transform);
        
    }

    public void SetRotationCenter(Transform playerTransform)
    {
        var position = playerTransform.position;
        _rotationCenterX = position.x;
        Debug.Log("Set center X to " + _rotationCenterX );
        _rotationCenterY = position.y;
        Debug.Log("Set center Y to " + _rotationCenterY );
        _rotationCenterZ = position.z;
        Debug.Log("Set center Z to " + _rotationCenterZ );
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("PlayerBullet")) return;
        Debug.Log("Hit by Bullet!");
        _rotationHeight = 0;
        _rotationWidth = 0;
        _targetSpeed = 0;
        _rotationCenterX = 20;
        Debug.Log("Set center X to " + _rotationCenterX );
        _rotationCenterY = 20;
        Debug.Log("Set center Y to " + _rotationCenterY );
        _rotationCenterZ = 20;
        Debug.Log("Set center Z to " + _rotationCenterZ );
        transform.position = new Vector3(_rotationCenterX, _rotationCenterY, _rotationCenterZ);
        gameObject.SetActive(false);
    }
    
}
