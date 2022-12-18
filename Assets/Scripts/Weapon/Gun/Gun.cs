using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    public UnityEvent OnGrab; // 총을 쥐었을 때 연결할 이벤트, 총을 쥐면 RayVisualizer 실행
    public UnityEvent OnRelease; // 쥔 총을 놓았을 때 연결할 이벤트, 총을 놓으면 RayVisualizer 해제, 거치대로 이동                              
    
    public void Grab(SelectEnterEventArgs args) // 총을 쥐었을 때의 이벤트에 연결할 함수
    {
        var interactor = args.interactorObject;
        if (interactor is XRDirectInteractor) OnGrab?.Invoke();
    }

    public void Release(SelectExitEventArgs args) // 쥔 총을 놓았을 때의 이벤트에 연결할 함수
    {
        var interactor = args.interactorObject;
        if (interactor is XRDirectInteractor) OnRelease?.Invoke();
    }
}
