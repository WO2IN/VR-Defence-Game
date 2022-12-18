using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TeleportActionHandler : MonoBehaviour
{
    public InputActionReference inputActionRef; // XRI Default Input Action

    public UnityEvent OnShow; // 텔레포테이션 시도할 때 광선을 보여주는 이벤트
    public UnityEvent OnHide; // 텔레포테이션 시도 완료 시 광선을 숨기는 이벤트

    private void OnEnable() // Ray Interactor 활성화
    {
        inputActionRef.action.performed += OnPerformed;
        inputActionRef.action.canceled += OnCanceled;
    }

    private void OnDisable() // Ray Interactor 비활성화
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

    private IEnumerator DelayCall(UnityEvent e) // 이벤트 호출 시 지연 시간
    {
        yield return null;
        e?.Invoke();
    }
}
