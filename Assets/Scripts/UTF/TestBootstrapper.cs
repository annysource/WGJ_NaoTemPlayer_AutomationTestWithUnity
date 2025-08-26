using UnityEngine;
using UnityEngine.XR;
using System.Collections;

public class TestBootstrapper : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(RunSmokeTests());
    }


    IEnumerator RunSmokeTests()
    {
        Debug.Log("<color=yellow>Iniciando Smoke Tests VR no runtime...</color>");

        yield return new WaitForSeconds(1f); 

       
        if (!XRSettings.isDeviceActive)
        {
            Debug.LogError("❌ Headset VR não está ativo.");
        }
        else
        {
            Debug.Log("✅ Headset VR detectado.");
        }

    
        var left = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.LeftHand);
        var right = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.RightHand);

        if (!left.isValid)
            Debug.LogError("❌ Controle esquerdo não detectado.");
        else
            Debug.Log("✅ Controle esquerdo detectado.");

        if (!right.isValid)
            Debug.LogError("❌ Controle direito não detectado.");
        else
            Debug.Log("✅ Controle direito detectado.");

        Debug.Log("<color=green>Smoke Tests concluídos.</color>");

       
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }

}
