using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    public UnityEvent OnGrab; // ���� ����� �� ������ �̺�Ʈ, ���� ��� RayVisualizer ����
    public UnityEvent OnRelease; // �� ���� ������ �� ������ �̺�Ʈ, ���� ������ RayVisualizer ����, ��ġ��� �̵�                              
    
    public void Grab(SelectEnterEventArgs args) // ���� ����� ���� �̺�Ʈ�� ������ �Լ�
    {
        var interactor = args.interactorObject;
        if (interactor is XRDirectInteractor) OnGrab?.Invoke();
    }

    public void Release(SelectExitEventArgs args) // �� ���� ������ ���� �̺�Ʈ�� ������ �Լ�
    {
        var interactor = args.interactorObject;
        if (interactor is XRDirectInteractor) OnRelease?.Invoke();
    }
}
