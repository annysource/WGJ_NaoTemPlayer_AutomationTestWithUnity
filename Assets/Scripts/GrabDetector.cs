using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabDetector : MonoBehaviour
{
    public bool wasGrabbed;

    public void ChangeBool()
    {
        wasGrabbed = true;
    }
}
