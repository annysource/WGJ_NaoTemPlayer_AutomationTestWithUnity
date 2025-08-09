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
                        Debug.LogWarning("Bot�o UI n�o encontrado no targetButton.");
                    }
                }
                else
                {
                    Debug.LogWarning("TargetButton n�o atribu�do.");
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
                        Debug.LogWarning("XRGrabInteractable n�o encontrado no target.");
                    }
                }
                else
                {
                    Debug.LogWarning("TargetInteractable n�o atribu�do.");
                }
                break;
            default:
                Debug.LogWarning("A��o n�o implementada.");
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
            Debug.LogError("InteractionManager n�o est� atribu�do no XRGrabInteractable.");
            yield break;
        }

        var interactorGO = new GameObject("FakeInteractor");
        var interactor = interactorGO.AddComponent<XRDirectInteractor>();

        interactor.transform.position = interactable.transform.position + Vector3.forward * 0.1f;

        // Aguarde um frame para o interactor ser configurado
        yield return null;

        // Cast para as interfaces necess�rias
        interactionManager.SelectEnter((IXRSelectInteractor)interactor, (IXRSelectInteractable)interactable);

        yield return new WaitForSeconds(1f);

        interactionManager.SelectExit((IXRSelectInteractor)interactor, (IXRSelectInteractable)interactable);

        GameObject.Destroy(interactorGO);
    }
}
