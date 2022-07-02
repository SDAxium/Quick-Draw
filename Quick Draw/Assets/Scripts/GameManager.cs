using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameManager instance;
    public GameObject player;

    public bool sceneTravel = false;
    private AsyncOperation _scene;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _scene = SceneManager.LoadSceneAsync(1);
        _scene.allowSceneActivation = false;
        
        // InvokeRepeating(nameof(MehFire),3f,10f);
    }
    
    void Update()
    {
        if (sceneTravel)
        {
            _scene.allowSceneActivation = true;
        }
        
    }

    public void LoadNextScene()
    {
        sceneTravel = true;
    }
    
    public void Exit()
    {
        Application.Quit();
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
