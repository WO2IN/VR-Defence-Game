using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponStand : MonoBehaviour
{
    public void OnSocketed(SelectEnterEventArgs args)
    {
        // �ѿ� ����� IReloadable ��ũ��Ʈ ������Ʈ�� ������ ������ ����
        var reloadable = args.interactableObject.transform.GetComponent<IReloadable>();
        reloadable?.StartReload();
    }

    public void OnUnSocketed(SelectExitEventArgs args)
    {
        // �ѿ� ����� IReloadable ��ũ��Ʈ ������Ʈ�� ������ ������ ����
        var reloadable = args.interactableObject.transform.GetComponent<IReloadable>();
        reloadable?.StopReload();
    }
}
