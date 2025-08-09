using UnityEngine;
using System.Collections;
public class TestCaseExecutor : MonoBehaviour
{
    public IEnumerator ExecuteTest(TestCaseSO testCase)
    {
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
                    }
                    else
                    {
                        Debug.LogWarning("O bot�o n�o tem componente ITestable, acionando evento padr�o.");
                        // Alternativa: chamar evento ou m�todo no bot�o
                        var button = testCase.targetButton.GetComponent<UnityEngine.UI.Button>();
                        if (button != null)
                            button.onClick.Invoke();
                    }
                }
                else
                {
                    Debug.LogWarning("TargetButton n�o atribu�do.");
                }
                break;

            default:
                Debug.LogWarning("A��o n�o implementada.");
                break;
        }

        yield return null;
    }
}
