using UnityEngine;
using UnityEngine.XR;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class TestBootstrapperLogs : MonoBehaviour
{
    private string logPath;

    void Start()
    {
        logPath = Path.Combine(Application.persistentDataPath, "test-results.txt");
        StartCoroutine(RunSmokeTests());
    }

    IEnumerator RunSmokeTests()
    {
        Debug.Log("=== Iniciando Smoke Tests VR no runtime ===");

        using (StreamWriter writer = new StreamWriter(logPath, false))
        {
            writer.WriteLine("=== Smoke Test Resultados ===");

            yield return new WaitForSeconds(1f); 

            bool allPassed = true;

            
            if (!XRSettings.isDeviceActive)
            {
                Debug.LogError("❌ Headset não ativo");
                writer.WriteLine("❌ Headset não ativo");
                allPassed = false;
            }
            else
            {
                Debug.Log("✅ Headset detectado");
                writer.WriteLine("✅ Headset detectado");
            }

            
            var left = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.LeftHand);
            var right = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.RightHand);

            if (!left.isValid)
            {
                Debug.LogError("❌ Controle esquerdo não detectado");
                writer.WriteLine("❌ Controle esquerdo não detectado");
                allPassed = false;
            }
            else
            {
                writer.WriteLine("✅ Controle esquerdo detectado");
            }

            if (!right.isValid)
            {
                Debug.LogError("❌ Controlador direito não detectado");
                writer.WriteLine("❌ Controlador direito não detectado");
                allPassed = false;
            }
            else
            {
                writer.WriteLine("✅ Controle direito detectado");
            }

            string mainSceneName = "GameScene"; 
            Scene activeScene = SceneManager.GetActiveScene();

            if (activeScene.name != mainSceneName)
            {
                Debug.LogError($"❌ Cena principal não carregada. Cena atual: {activeScene.name}");
                writer.WriteLine($"❌ Cena principal não carregada. Cena atual: {activeScene.name}");
                allPassed = false;
            }
            else
            {
                Debug.Log($"✅ Cena principal '{mainSceneName}' carregada corretamente");
                writer.WriteLine($"✅ Cena principal '{mainSceneName}' carregada corretamente");
            }

            writer.WriteLine("Resultado Final: " + (allPassed ? "✅ PASSOU" : "❌ FALHOU"));
        }

        Debug.Log("Testes concluídos. Encerrando app.");
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
