using System;
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

    public void StartTests()
    {
        StartCoroutine(RunAllTests());
    }

    public IEnumerator RunAllTests()
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
                yield return StartCoroutine(executor.ExecuteTest(currentTest, result => passed = result));
            }
            else
            {
                Debug.LogError("TestCaseExecutor não encontrado.");
            }

            string resultText = passed ? "PASS" : "FAIL";
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


    public IEnumerator ExecuteTest(TestCaseSO testCase)
    {
        
        TestCaseExecutor executor = GetComponent<TestCaseExecutor>();
        if (executor != null)
        {
            bool passed = false;
            yield return executor.ExecuteTest(testCase, result => passed = result);
            // Aqui você pode adicionar validação e logging de sucesso/falha
            Debug.Log($"Teste {(passed ? "PASSOU" : "FALHOU")}: (testCase.testName");
        }
        else
        {
            Debug.LogError("TestCaseExecutor não encontrado no GameObject.");
        }
    }
}
