using UnityEngine;
using UnityEngine.XR;
using System.Collections;

public class TestBootstrapper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RunSmokeTests());
    }


    IEnumerator RunSmokeTests()
    {
        Debug.Log("<color=yellow>Iniciando Smoke Tests VR no runtime...</color>");

        yield return new WaitForSeconds(1f); // Espera XR inicializar

        // Teste 1: Headset conectado
        if (!XRSettings.isDeviceActive)
        {
            Debug.LogError("❌ Headset VR não está ativo.");
        }
        else
        {
            Debug.Log("✅ Headset VR detectado.");
        }

        // Teste 2: Controladores
        var left = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.LeftHand);
        var right = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.RightHand);

        if (!left.isValid)
            Debug.LogError("❌ Controlador esquerdo não detectado.");
        else
            Debug.Log("✅ Controlador esquerdo detectado.");

        if (!right.isValid)
            Debug.LogError("❌ Controlador direito não detectado.");
        else
            Debug.Log("✅ Controlador direito detectado.");

        Debug.Log("<color=green>Smoke Tests concluídos.</color>");

        // Opcional: encerrar app após os testes
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }

}
