using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RayVisualizer : MonoBehaviour
{
    // Ray
    public LineRenderer ray; // 광선
    public LayerMask hitRayMask; // 레이캐스팅할 오브젝트들이 배치된 레이어
    public float distance = 100f; // 레이저캐스트 사정거리

    // Reticle Point
    public GameObject reticlePoint; // 광선 끝 지점을 나타내는 오브젝트
    public bool showReticle = true; // 화면 출력 여부

    private void Awake()
    {
        Off();
    }

    public void On()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }

    public void Off()
    {
        StopAllCoroutines();
        ray.enabled = false;
        reticlePoint.SetActive(false);
    }

    public IEnumerator Process()
    {
        while (true)
        {
            // 레이캐스팅(광선을 쐈을 때 방향과 거리 내에 충돌이 감지된 오브젝트가 있다면 true 리턴)
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, distance, hitRayMask))
            {
                // 광선의 끝점 설정 --> raticlePoint
                ray.SetPosition(1, transform.InverseTransformPoint(hitInfo.point));
                ray.enabled = true;

                reticlePoint.transform.position = hitInfo.point;
                reticlePoint.SetActive(showReticle);
            }
            else
            {
                ray.enabled = false;
                reticlePoint.SetActive(false);
            }
            yield return null;
        }
    }
}
