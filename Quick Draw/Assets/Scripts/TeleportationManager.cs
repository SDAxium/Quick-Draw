using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class TeleportationManager : MonoBehaviour
{
    public GameObject baseController, teleportationGameObject;

    public InputActionReference teleportActivationReference;

    public UnityEvent onTeleportActivate;
    public UnityEvent onTeleportCancel;
    
    private void Start()
    {
        teleportActivationReference.action.performed += TeleportModeActivate;
        teleportActivationReference.action.canceled += TeleportModeCancel;
    }  

    private void TeleportModeActivate(InputAction.CallbackContext obj)
    {
       onTeleportActivate.Invoke();
    }

    private void DeactivateTeleporter()
    {
        onTeleportCancel.Invoke();
    }
    private void TeleportModeCancel(InputAction.CallbackContext obj)
    {
        Invoke(nameof(DeactivateTeleporter),.1f);
    }
}
