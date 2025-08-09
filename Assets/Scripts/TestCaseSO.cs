using UnityEngine;

[CreateAssetMenu(fileName = "NewTestCase", menuName = "QA/Test Case")]
public class TestCaseSO : ScriptableObject
{
    public string testName;
    public TestActionType actionType;
    public GameObject targetButton;  // objeto a ser pressionado
    public GameObject targetInteractable; //grab
    [TextArea]
    public string expectedResultDescription;
}
