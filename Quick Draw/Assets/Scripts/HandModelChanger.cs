using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class HandModelChanger : MonoBehaviour
{
    [SerializeField] private InputActionReference handModelChangeButton;
    
    private void OnEnable()
    {
        //handModelChangeButton.action.performed += ActivateTeleport;
        //handModelChangeButton.action.canceled += DeactivateTeleport;
    }
    private void OnDisable()
    {
        //handModelChangeButton.action.performed -= ActivateTeleport;
        //handModelChangeButton.action.canceled -= DeactivateTeleport;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
