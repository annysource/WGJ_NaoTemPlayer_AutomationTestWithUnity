using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManagerSO : MonoBehaviour
{
    [Tooltip("Lista de casos de teste scriptable objects")]
    public List<TestCaseSO> testCases;

    private int currentIndex = 0;

    public IEnumerator RunAllTests()
    {
        while (currentIndex < testCases.Count)
        {
            TestCaseSO currentTest = testCases[currentIndex];
            Debug.Log($"Executando teste {currentTest.testName}");

            // Executa o teste, assumindo que você tenha um executor separado
            yield return StartCoroutine(ExecuteTest(currentTest));

            currentIndex++;
        }

        Debug.Log("Todos os testes finalizados.");
        currentIndex = 0;
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
