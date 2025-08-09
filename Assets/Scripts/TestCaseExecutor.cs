using UnityEngine;
using System.Collections;
public class TestCaseExecutor : MonoBehaviour
{
    public IEnumerator ExecuteTest(TestCaseSO testCase, System.Action<bool> callback)
    {
        bool passed = false;
        Debug.Log($"Iniciando teste: {testCase.testName}");

        switch (testCase.actionType)
        {
            case TestActionType.PressButton:
                if (testCase.targetButton != null)
                {
                    // Se tiver um componente ITestable, chama Perform
                    var testable = testCase.targetButton.GetComponent<ITestable>();
                    if (testable != null)
                    {
                        testable.Perform("press");
                        Debug.Log("Botão pressionado via Testable.");
                        passed = true;
                    }
                    else
                    {
                        Debug.LogWarning("O botão não tem componente ITestable, acionando evento padrão.");
                        var button = testCase.targetButton.GetComponent<UnityEngine.UI.Button>();
                        if (button != null)
                        {
                            button.onClick.Invoke();
                            passed = true;
                        }
                        else
                        {
                            Debug.LogWarning("O botão UI nao encontrado no targetButton.");
                        }

                    }
                }
                else
                {
                    Debug.LogWarning("TargetButton nao atribuído");
                }
                break;

            default:
                Debug.LogWarning("Ação não implementada.");
                break;
        }

        yield return new WaitForSeconds(0.5f);
        callback?.Invoke(passed);
    }
}
