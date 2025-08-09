using UnityEngine;

public enum TestActionType
{
    PressButton,
    // você pode adicionar outros tipos depois
}

[CreateAssetMenu(fileName = "NewTestCase", menuName = "QA/Test Case")]
public class TestCaseSO : ScriptableObject
{
    public string testName;
    public TestActionType actionType;
    public GameObject targetButton;  // objeto a ser pressionado

    [TextArea]
    public string expectedResultDescription;
}
