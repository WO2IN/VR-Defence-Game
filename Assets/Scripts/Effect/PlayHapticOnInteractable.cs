using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayHapticOnInteractable : MonoBehaviour
{
    public float amplitude = 0.05f; // ����
    public float duration = 0.05f; // �ֱ�

    private XRBaseInteractable target; // ���ͷ��ͺ� ������Ʈ ���� ����

    private void Awake()
    {
        target = GetComponent<XRBaseInteractable>(); // ����       
    }

    public void Call()
    {
        var interactor = target.firstInteractorSelecting as XRBaseControllerInteractor; // ���ͷ��� ���� ����

        if (interactor.xrController == null) return; // ���ͷ��Ϳ� �ش��ϴ� ��Ʈ�ѷ� ���� ��������� ����

        interactor.xrController.SendHapticImpulse(amplitude, duration); // ���ͷ��� ��Ʈ�ѷ��� ������ �ֱ⸸ŭ ó��
    }
}
