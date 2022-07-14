using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class HitTarget : MonoBehaviour
{
    public AudioClip blindPing;
    public AudioClip spawnSound;

    public GameObject player;
    private float _targetSpeed;

    private float _rotationHeight;
    private float _rotationWidth;
    
    private float _timer;

    public float rotationCenterX;
    public float rotationCenterY;
    public float rotationCenterZ;

    private int _oscillationCase;

    public void SetNewRandomValues()
    {
        if (!player)
        {
            player = GameObject.Find("Player");
        }
        _timer = Random.Range(0, 181);
        _targetSpeed = Random.Range(1, 2);
        _rotationHeight = Random.Range(2, 15);
        _rotationWidth = Random.Range(5, 16);
        _oscillationCase = Random.Range(1, 4);
        SetRotationCenter(player.transform);
    }
    public void UpdateLocation()
    {
        _timer += Time.deltaTime*_targetSpeed;
        float horizontalRotation;
        float verticalRotation;

        switch (_oscillationCase)
        {
            case 1:
                horizontalRotation = Mathf.Cos(_timer) * _rotationWidth + rotationCenterX;
                verticalRotation = Mathf.Sin(_timer) * _rotationHeight + rotationCenterY;
                transform.position = new Vector3(horizontalRotation, verticalRotation, rotationCenterZ);
                break;
            case 2:
                horizontalRotation = Mathf.Cos(_timer) * _rotationWidth + rotationCenterX;
                verticalRotation = Mathf.Sin(_timer) * _rotationHeight + rotationCenterZ;
                transform.position = new Vector3(horizontalRotation, rotationCenterY, verticalRotation);
                break;
            case 3:
                horizontalRotation = Mathf.Cos(_timer) * _rotationWidth + rotationCenterY;
                verticalRotation = Mathf.Sin(_timer) * _rotationHeight + rotationCenterZ;
                transform.position = new Vector3(rotationCenterX, verticalRotation, horizontalRotation);
                break;
            default:
                horizontalRotation = Mathf.Cos(_timer) * _rotationWidth + rotationCenterX;
                verticalRotation = Mathf.Sin(_timer) * _rotationHeight + rotationCenterY;
                transform.position = new Vector3(horizontalRotation, verticalRotation, rotationCenterZ);
                break;
        }
        transform.LookAt(new Vector3(rotationCenterX,rotationCenterY,rotationCenterZ));
        
    }

    private void SetRotationCenter(Transform playerTransform)
    {
        var position = playerTransform.position;
        rotationCenterX = position.x;
        rotationCenterY = position.y;
        rotationCenterZ = position.z;
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("PlayerBullet")) return;
        Debug.Log("Hit by Bullet!");
        _rotationHeight = 0;
        _rotationWidth = 0;
        _targetSpeed = 0;
        rotationCenterX = 20;
        rotationCenterY = 20;
        rotationCenterZ = 20;
        transform.position = new Vector3(rotationCenterX, rotationCenterY, rotationCenterZ);
        gameObject.SetActive(false);
    }
    
}
