using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetController : MonoBehaviour
{
    public List<GameObject> inactiveTargets = new List<GameObject>();
    public List<GameObject> activeTargets = new List<GameObject>();

    public GameObject bulletTargetPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartSimulation();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTargets.Count <= 0) return;

        foreach (var target in activeTargets)
        {
            target.GetComponent<BulletTarget>().UpdateLocation();
            if (CheckIfTargetOutOfRange(target))
            {
                PutAwayTarget(target);
            }
        }
    }

    private IEnumerator TargetSpawning()
    {
        while (activeTargets.Count < 20)
        {
            Debug.Log("Spawning new Target");
            GameObject bt = Instantiate(bulletTargetPrefab);
            BulletTarget btScript = bt.GetComponent<BulletTarget>();
            btScript.SetNewRandomValues();
            activeTargets.Add(bt);
            Debug.Log("Target Spawned at: " + bt.transform.position);
            yield return new WaitForSeconds(3f);
        }
    }

    private bool CheckIfTargetOutOfRange(GameObject target)
    {
        var position = target.transform.position;
        float x = Mathf.Abs(position.x);
        float y = Mathf.Abs(position.y);
        float z = Mathf.Abs(position.z);

        return x > 50 || y > 50 || z > 50;
    }
    private void PutAwayTarget(GameObject target)
    {
        activeTargets.Remove(target);
        inactiveTargets.Add(target);
        target.SetActive(false);
    }
    
    public void StartSimulation()
    {
        StartCoroutine(TargetSpawning());
    }
    
    public void EndSimulation()
    {
        StopCoroutine(TargetSpawning());
    }
}
