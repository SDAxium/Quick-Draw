using UnityEngine;
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

    public bool targetActive;

    // public bool notMoving;
    private void Awake()
    {
        targetActive = true;
    }

    private void Start()
    { 
        // notMoving = true;
        // TargetController ts = GameObject.Find("Target Controller").GetComponent<TargetController>();
        // if (!ts.activeTargets.Contains(gameObject))
        // {
        //      ts.activeTargets.Add(gameObject);
        // }
    }

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
        // if (notMoving)
        // {
        //     return;
        // }
        _timer += Time.deltaTime*_targetSpeed;
        float horizontalRotation;
        float verticalRotation;

        switch (_oscillationCase)
        {
            case 1:
                horizontalRotation = Mathf.Cos(_timer) * _rotationWidth + rotationCenterX;
                verticalRotation = Mathf.Sin(_timer) * _rotationHeight + rotationCenterY;
                transform.position = new Vector3(horizontalRotation, verticalRotation, rotationCenterZ);
                transform.LookAt(player.transform);
                transform.Rotate(Vector3.right,90f);
                break;
            case 2:
                horizontalRotation = Mathf.Cos(_timer) * _rotationWidth + rotationCenterX;
                verticalRotation = Mathf.Sin(_timer) * _rotationHeight + rotationCenterZ;
                transform.position = new Vector3(horizontalRotation, rotationCenterY, verticalRotation);
                transform.LookAt(player.transform);
                transform.Rotate(Vector3.right,90f);
                break;
            case 3:
                horizontalRotation = Mathf.Cos(_timer) * _rotationWidth + rotationCenterY;
                verticalRotation = Mathf.Sin(_timer) * _rotationHeight + rotationCenterZ;
                transform.position = new Vector3(rotationCenterX, verticalRotation, horizontalRotation);
                transform.LookAt(player.transform);
                transform.Rotate(Vector3.right,90f);
                break;
            default:
                horizontalRotation = Mathf.Cos(_timer) * _rotationWidth + rotationCenterX;
                verticalRotation = Mathf.Sin(_timer) * _rotationHeight + rotationCenterY;
                transform.position = new Vector3(horizontalRotation, verticalRotation, rotationCenterZ);
                transform.LookAt(player.transform);
                transform.Rotate(Vector3.right,90f);
                break;
        }
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
        if (!other.gameObject.CompareTag("Bullet")) return;
        targetActive = false;
    }
    
}
