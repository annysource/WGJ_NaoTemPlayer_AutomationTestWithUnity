using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ButtonPressDetector : MonoBehaviour
{
    public bool wasPressed = false;
    private XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnPressed);
        }
        else
        {
            Debug.LogWarning("XRBaseInteractable n�o encontrado no bot�o VR.");
        }
    }

    public void OnPressed(SelectEnterEventArgs args)
    {
        wasPressed = true;
        Debug.Log($"Bot�o VR {gameObject.name} foi pressionado.");
    }

    private void OnDestroy()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnPressed);
    }
}
