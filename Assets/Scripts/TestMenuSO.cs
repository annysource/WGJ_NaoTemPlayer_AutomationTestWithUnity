using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMenuSO : MonoBehaviour
{
    public TestManagerSO testManager;
    public GameObject togglePrefab;
    public Transform toggleParent;
    public Button runButton;

    private List<Toggle> toggles = new List<Toggle>();

    void Start()
    {
        PopulateToggleList();
        runButton.onClick.AddListener(RunSelectedTests);
    }

    void PopulateToggleList()
    {
        foreach (var testCase in testManager.testCases)
        {
            GameObject toggleGO = Instantiate(togglePrefab, toggleParent);
            Toggle toggle = toggleGO.GetComponent<Toggle>();
            toggle.GetComponentInChildren<Text>().text = testCase.testName;
            toggles.Add(toggle);
        }
    }

    void RunSelectedTests()
    {
        List<TestCaseSO> selectedTests = new List<TestCaseSO>();
        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i].isOn)
                selectedTests.Add(testManager.testCases[i]);
        }

        if (selectedTests.Count > 0)
        {
            StartCoroutine(RunSelectedTestsCoroutine(selectedTests));
        }
        else
        {
            Debug.LogWarning("Nenhum teste selecionado.");
        }
    }

    IEnumerator RunSelectedTestsCoroutine(List<TestCaseSO> selectedTests)
    {
        foreach (var test in selectedTests)
        {
            yield return testManager.ExecuteTest(test);
        }
        Debug.Log("Execução dos testes selecionados finalizada.");
    }
}
