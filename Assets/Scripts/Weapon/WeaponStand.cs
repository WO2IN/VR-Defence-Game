using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponStand : MonoBehaviour
{
    public void OnSocketed(SelectEnterEventArgs args)
    {
        // 총에 연결된 IReloadable 스크립트 컴포넌트가 있으면 재충전 시작
        var reloadable = args.interactableObject.transform.GetComponent<IReloadable>();
        reloadable?.StartReload();
    }

    public void OnUnSocketed(SelectExitEventArgs args)
    {
        // 총에 연결된 IReloadable 스크립트 컴포넌트가 있으면 재충전 중지
        var reloadable = args.interactableObject.transform.GetComponent<IReloadable>();
        reloadable?.StopReload();
    }
}
