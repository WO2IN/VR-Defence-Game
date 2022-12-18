using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RayVisualizer : MonoBehaviour
{
    // Ray
    public LineRenderer ray; // ����
    public LayerMask hitRayMask; // ����ĳ������ ������Ʈ���� ��ġ�� ���̾�
    public float distance = 100f; // ������ĳ��Ʈ �����Ÿ�

    // Reticle Point
    public GameObject reticlePoint; // ���� �� ������ ��Ÿ���� ������Ʈ
    public bool showReticle = true; // ȭ�� ��� ����

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
            // ����ĳ����(������ ���� �� ����� �Ÿ� ���� �浹�� ������ ������Ʈ�� �ִٸ� true ����)
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, distance, hitRayMask))
            {
                // ������ ���� ���� --> raticlePoint
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
