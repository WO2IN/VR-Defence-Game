using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // �̺�Ʈ ���̺귯�� �߰�

public class Shooter : MonoBehaviour
{
    public LayerMask hittableMask; // ���ͷ��� ������ ���� ���̾��ũ
    public GameObject hitEffectPrefab; // ������ �¾��� �� �ʿ��� ����Ʈ ������Ʈ
    public Transform shootPoint; // �ѱ� �� ����

    public float shootDelay = 0.1f; // �� �߻� ����
    public float maxDistance = 100f; // �ִ�Ÿ�

    public UnityEvent<Vector3> OnShootSuccess; // �� �߻� ���� �� ����Ʈ
    public UnityEvent OnShootFail; // �� �߻� ���� �� ����Ʈ

    public Magazine magazine; // Magazine ������Ʈ ���� ���� ����

    private void Awake()
    {
        magazine = GetComponent<Magazine>();    
    }

    private void Start()
    {
        Stop(); // ��� ���� ���߱�
    }


    public void Stop()
    {
        StopAllCoroutines(); // ��� �ڸ�ƾ ����
    }
    public void Play()
    {
        StopAllCoroutines(); // Play() �Լ� ȣ�� �� �������� ��� �ڸ�ƾ ����
        StartCoroutine(Process()); // Process() �ڸ�ƾ �Լ� ����
    }

    public IEnumerator Process() // �ڸ�ƾ �Լ�
    {
        var wfs = new WaitForSeconds(shootDelay); // �����ð� ��ü ����

        while (true)
        {
            if (magazine.Use()) // źâ�� ������ 1�� ����
                Shoot(); // �߻�
            else // źâ�� ������
                OnShootFail?.Invoke(); // �߻� ���� �̺�Ʈ �Լ� ȣ��

            yield return wfs; // shootDelay ��ŭ ���
        }
    } 

    private void Shoot()
    {
        // ����ĳ���� ���� �� (������ ���� ��) �������ο� ���� ������ �����Ͽ� �̺�Ʈ �Լ� ȣ��
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hitInfo, maxDistance, hittableMask))
        {
            Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.identity); // ����ĳ���� ���� �� ����Ʈ������ ������Ʈ ����

            var hitObject = hitInfo.transform.GetComponent<Hittable>();
            hitObject?.Hit();

            OnShootSuccess?.Invoke(hitInfo.point); // �ѿ� ���� ��� ������Ʈ ��ġ�� �����Ͽ� OnShootSucess �̺�Ʈ �Լ� ȣ��
        }
        else
        {
            var hitPoint = shootPoint.position + shootPoint.forward * maxDistance; // �ѿ� ���� �ʾ��� �� ��� ��ġ �뷫 ����
            OnShootSuccess?.Invoke(hitPoint); // hitPoint�� �Բ� OnShootSucess �̺�Ʈ �Լ� ȣ��
        }
    }
}
