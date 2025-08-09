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
                        Debug.Log("Bot�o pressionado via Testable.");
                        passed = true;
                    }
                    else
                    {
                        Debug.LogWarning("O bot�o n�o tem componente ITestable, acionando evento padr�o.");
                        var button = testCase.targetButton.GetComponent<UnityEngine.UI.Button>();
                        if (button != null)
                        {
                            button.onClick.Invoke();
                            passed = true;
                        }
                        else
                        {
                            Debug.LogWarning("O bot�o UI nao encontrado no targetButton.");
                        }

                    }
                }
                else
                {
                    Debug.LogWarning("TargetButton nao atribu�do");
                }
                break;

            default:
                Debug.LogWarning("A��o n�o implementada.");
                break;
        }

        yield return new WaitForSeconds(0.5f);
        callback?.Invoke(passed);
    }
}
