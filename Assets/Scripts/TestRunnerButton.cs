using UnityEngine;

public class TestRunnerButton : MonoBehaviour
{
    public TestManagerSO testManager;

    public void OnRunTestsButtonClicked()
    {
        if (testManager != null)
        {
            testManager.StartTests();
        }
        else
        {
            Debug.LogError("TestManagerSO não está atribuído no TestRunnerButton.");
        }
    }
}
