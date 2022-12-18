using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class ReturnToTarget : MonoBehaviour
{
    public Transform target; // ��ġ�� ��ġ

    public float duration = 1f; // ��ġ��� ���� �̵��ϴ� �ð�
    public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f); // �ε巴�� �̵�

    public UnityEvent OnCompleted; // �̵� �Ϸ����� �� �����ϴ� �Լ� ����

    public void Call()
    {
        if (!gameObject.activeInHierarchy) return; // ���� ������Ʈ�� ��Ȱ��ȭ ���¶�� ����

        StopAllCoroutines(); // ���� ������Ʈ�� Ȱ��ȭ ���¶�� ��� �ڷ�ƾ �Լ� ����
        StartCoroutine(Process()); // Process() �ڷ�ƾ �Լ� ȣ��
    }

    public IEnumerator Process()
    {
        if (target == null) yield break; // ��ǥ ��ġ�� ����ִٸ� �ڷ�ƾ �Լ� ����

        var beginTime = Time.time; // �ݺ� ���� �ð�

        while (true)
        {
            var t = (Time.time - beginTime) / duration; // ��ü 1�� �� �󸶳� ����Ǿ�����(%) ����

            if (t >= 1f) break; // 100% ����Ǿ��ٸ� �ݺ��� ���

            t = curve.Evaluate(t); // �� % ����Ǿ������� curve ������ ��ȯ

            transform.position = Vector3.Lerp(transform.position, target.position, t); // ��ġ ������ ����(���ۿ��� ��ǥ �� ��� ��ġ����)

            yield return null;
        }

        transform.position = target.position; // t�� 100% ����Ǿ��ٸ� ���� ������Ʈ�� ��ǥ ��ġ�� ����

        OnCompleted?.Invoke(); // OnCompleted �̺�Ʈ�� ����� �Լ� ���� (������)
    }
}
