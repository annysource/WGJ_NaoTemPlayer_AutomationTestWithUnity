using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestManagerSO : MonoBehaviour
{
    [Tooltip("Lista de casos de teste scriptable objects")]
    public List<TestCaseSO> testCases;
    public TMP_Text statusText;
    private List<string> testResults = new List<string>();

    private void Start()
    {
        ShowTestCasesList();
    }
    public void StartTests()
    {
        StartCoroutine(RunAllTests());
    }

    public void ShowTestCasesList()
    {
        statusText.text = "Casos de Teste Carregados:\n";
        foreach (var testCase in testCases)
        {
            statusText.text += "- " + testCase.testName + "\n";
        }
    }

    private IEnumerator RunAllTests()
    {
        testResults.Clear();
        for (int i = 0; i < testCases.Count; i++)
        {
            TestCaseSO currentTest = testCases[i];
            statusText.text = $"Executando teste: {currentTest.testName}";
            Debug.Log($"Executando teste {currentTest.testName}");

            bool passed = false;

            TestCaseExecutor executor = GetComponent<TestCaseExecutor>();
            if (executor != null)
            {
                yield return executor.ExecuteTest(currentTest, result => passed = result);
            }
            else
            {
                Debug.LogError("TestCaseExecutor não encontrado.");
            }

            string resultText = passed ? "<color=green>PASS</color>" : "<color=red>FAIL</color>";
            testResults.Add($"{currentTest.testName} - {resultText}");

            statusText.text = $"Testes executados: {i + 1}/{testCases.Count}\n";
            foreach (var r in testResults)
            {
                statusText.text += r + "\n";
            }
        }
        statusText.text += "\nTodos os testes finalizados.";
        Debug.Log("Todos os testes finalizados.");
    }

    public void ExecuteSingleTest(TestCaseSO testCase, System.Action<bool> callback)
    {
        StartCoroutine(ExecuteSingleTestCoroutine(testCase, callback));
    }

    public IEnumerator ExecuteSingleTestCoroutine(TestCaseSO testCase, System.Action<bool> callback)
    {
        TestCaseExecutor executor = GetComponent<TestCaseExecutor>();
        if (executor != null)
        {
            yield return executor.ExecuteTest(testCase, callback);
        }
        else
        {
            Debug.LogError("TestCaseExecutor não encontrado.");
            callback?.Invoke(false);
        }
    }
}
