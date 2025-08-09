using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;
public enum TestActionType
{
    PressButton,
    GrabObject,
    // outros
}

public class TestCaseExecutor : MonoBehaviour
{
    public IEnumerator ExecuteTest(TestCaseSO testCase, System.Action<bool> callback)
    {
        bool passed = false;

        switch (testCase.actionType)
        {
            case TestActionType.PressButton:
                if (testCase.targetButton != null)
                {
                    var button = testCase.targetButton.GetComponent<Button>();
                    if (button != null)
                    {
                        button.onClick.Invoke();
                        passed = true;
                    }
                    else
                    {
                        Debug.LogWarning("Botão UI não encontrado no targetButton.");
                    }
                }
                else
                {
                    Debug.LogWarning("TargetButton não atribuído.");
                }
                break;

            case TestActionType.GrabObject:
                if (testCase.targetInteractable != null)
                {
                    var grabInteractable = testCase.targetInteractable.GetComponent<XRGrabInteractable>();
                    if (grabInteractable != null)
                    {
                        yield return GrabAndRelease(grabInteractable);
                        passed = true;
                    }
                    else
                    {
                        Debug.LogWarning("XRGrabInteractable não encontrado no target.");
                    }
                }
                else
                {
                    Debug.LogWarning("TargetInteractable não atribuído.");
                }
                break;
            default:
                Debug.LogWarning("Ação não implementada.");
                break;
        }

        yield return new WaitForSeconds(0.5f);

        callback?.Invoke(passed);
    }
    private IEnumerator GrabAndRelease(XRGrabInteractable interactable)
    {
        var interactionManager = interactable.interactionManager;
        if (interactionManager == null)
        {
            Debug.LogError("InteractionManager não está atribuído no XRGrabInteractable.");
            yield break;
        }

        var interactorGO = new GameObject("FakeInteractor");
        var interactor = interactorGO.AddComponent<XRDirectInteractor>();

        interactor.transform.position = interactable.transform.position + Vector3.forward * 0.1f;

        // Aguarde um frame para o interactor ser configurado
        yield return null;

        // Cast para as interfaces necessárias
        interactionManager.SelectEnter((IXRSelectInteractor)interactor, (IXRSelectInteractable)interactable);

        yield return new WaitForSeconds(1f);

        interactionManager.SelectExit((IXRSelectInteractor)interactor, (IXRSelectInteractable)interactable);

        GameObject.Destroy(interactorGO);
    }
}
