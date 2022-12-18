using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayHapticOnInteractable : MonoBehaviour
{
    public float amplitude = 0.05f; // 진동
    public float duration = 0.05f; // 주기

    private XRBaseInteractable target; // 인터랙터블 컴포넌트 참조 변수

    private void Awake()
    {
        target = GetComponent<XRBaseInteractable>(); // 참조       
    }

    public void Call()
    {
        var interactor = target.firstInteractorSelecting as XRBaseControllerInteractor; // 인터랙터 참조 변수

        if (interactor.xrController == null) return; // 인터랙터에 해당하는 컨트롤러 값이 비어있으면 종료

        interactor.xrController.SendHapticImpulse(amplitude, duration); // 인터랙션 컨트롤러에 진동을 주기만큼 처리
    }
}
