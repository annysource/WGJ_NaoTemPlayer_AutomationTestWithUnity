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
                        Debug.Log("Botão pressionado via Testable.");
                    }
                    else
                    {
                        Debug.LogWarning("O botão não tem componente ITestable, acionando evento padrão.");
                        // Alternativa: chamar evento ou método no botão
                        var button = testCase.targetButton.GetComponent<UnityEngine.UI.Button>();
                        if (button != null)
                            button.onClick.Invoke();
                    }
                }
                else
                {
                    Debug.LogWarning("TargetButton não atribuído.");
                }
                break;

            default:
                Debug.LogWarning("Ação não implementada.");
                break;
        }

        yield return null;
    }
}
