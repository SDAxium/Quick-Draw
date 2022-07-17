using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public List<GameObject> inactiveTargets = new List<GameObject>();
    public List<GameObject> activeTargets = new List<GameObject>();

    public GameObject bulletTargetPrefab;

    public bool simulationOn = false;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(TargetSpawning());
        //Invoke(nameof(StartSimulation),0.5f);
    }

    // Update is called once per frame
    public void Update()
    {
        if (activeTargets.Count <= 0) return;
        // ReSharper disable once ForCanBeConvertedToForeach
        for(var index = 0; index < activeTargets.Count; index++)
        {
            var target = activeTargets[index]; // The target parent object
            // Target child object. The parent is being used because all of its transforms are at default
           // var targetChild = target.transform.GetChild(0).gameObject; 
            if (!target.GetComponent<HitTarget>().targetActive)
            {
                Debug.Log("HIT");
                PutAwayTarget(target);
                continue;
            }
            target.GetComponent<HitTarget>().UpdateLocation();
        }
    }
    
    private IEnumerator TargetSpawning()
    {
        if (!simulationOn)
        {
            StopCoroutine(nameof(TargetSpawning));
            yield break;
        }
        
        // If active target list is full, wait until it isn't
        if (activeTargets.Count >= 20) yield return new WaitUntil(() => activeTargets.Count < 20); 
       
        GameObject bulletTarget;
        if (inactiveTargets.Count > 0)
        {
            bulletTarget = inactiveTargets[0];
            inactiveTargets.Remove(bulletTarget);
        }
        else
        {
            bulletTarget = Instantiate(bulletTargetPrefab);
        }
        bulletTarget.GetComponent<HitTarget>().SetNewRandomValues();
        activeTargets.Add(bulletTarget);
        bulletTarget.SetActive(true);
        yield return new WaitForSeconds(3f);
        yield return TargetSpawning();
    }
    
    private void PutAwayTarget(GameObject target)
    {
        activeTargets.Remove(target); // Remove from active targets
        inactiveTargets.Add(target); // Add to inactive targets
        target.SetActive(false); // Turn target visibility off
    }
    
    public void StartSimulation()
    {
        simulationOn = true;
        StartCoroutine(TargetSpawning());
    }
    
    public void EndSimulation()
    {
        simulationOn = false;
        for(var i = activeTargets.Count-1; i > 0;i--)
        {
            var target = activeTargets[i];
            PutAwayTarget(target);
        }
    }
}
