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

    public bool stopSimul = true;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(TargetSpawning());
        Invoke(nameof(StartSimulation),5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTargets.Count > 0)
        {
            foreach (var target in activeTargets)
            {
                target.GetComponent<HitTarget>().UpdateLocation();
            }
        }
        /*if (activeTargets.Count >= 20)
        {
            for(var i = activeTargets.Count-1; i > 0;i--)
            {
                var target = activeTargets[i];
                PutAwayTarget(target);
            }
        }*/
    }
    
    private IEnumerator TargetSpawning()
    {
        if (stopSimul)
        {
            StopCoroutine(nameof(TargetSpawning));
            yield break;
        }
        if (activeTargets.Count >= 20) yield return new WaitUntil(() => activeTargets.Count < 20);
        GameObject bulletTarget;
        HitTarget btScript;
        if (inactiveTargets.Count > 0)
        {
            bulletTarget = inactiveTargets[0];
            bulletTarget.SetActive(true);
            btScript = bulletTarget.GetComponent<HitTarget>();
            btScript.SetNewRandomValues();
            inactiveTargets.Remove(bulletTarget);
        }
        else
        {
            bulletTarget = Instantiate(bulletTargetPrefab);
            btScript = bulletTarget.GetComponent<HitTarget>();
            btScript.SetNewRandomValues();
            
        }
        activeTargets.Add(bulletTarget);
        yield return new WaitForSeconds(3f);
        yield return TargetSpawning();
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
        Debug.Log("Putting Away a Target");
        activeTargets.Remove(target);
        inactiveTargets.Add(target);
        target.SetActive(false);
    }
    
    public void StartSimulation()
    {
        Debug.Log("Starting Simulation");
        stopSimul = false;
        StartCoroutine(TargetSpawning());
    }
    
    public void EndSimulation()
    {
        Debug.Log("Ending Simulation");
        stopSimul = true;
        for(var i = activeTargets.Count-1; i > 0;i--)
        {
            var target = activeTargets[i];
            PutAwayTarget(target);
        }
    }
}
