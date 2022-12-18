using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TeleportActionHandler : MonoBehaviour
{
    public InputActionReference inputActionRef; // XRI Default Input Action

    public UnityEvent OnShow; // �ڷ������̼� �õ��� �� ������ �����ִ� �̺�Ʈ
    public UnityEvent OnHide; // �ڷ������̼� �õ� �Ϸ� �� ������ ����� �̺�Ʈ

    private void OnEnable() // Ray Interactor Ȱ��ȭ
    {
        inputActionRef.action.performed += OnPerformed;
        inputActionRef.action.canceled += OnCanceled;
    }

    private void OnDisable() // Ray Interactor ��Ȱ��ȭ
    {
        inputActionRef.action.performed -= OnPerformed;
        inputActionRef.action.canceled -= OnCanceled;
    }

    private void OnPerformed(InputAction.CallbackContext obj)
    {
        StartCoroutine(DelayCall(OnShow));
    }

    private void OnCanceled(InputAction.CallbackContext obj)
    {
        StartCoroutine(DelayCall(OnHide));
    }

    private IEnumerator DelayCall(UnityEvent e) // �̺�Ʈ ȣ�� �� ���� �ð�
    {
        yield return null;
        e?.Invoke();
    }
}
