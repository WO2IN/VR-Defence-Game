using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinePosition : MonoBehaviour
{
    public int index; // ������ �ε���

    private LineRenderer target; // ���η����� ������Ʈ ���� ����

    private void Awake()
    {
        target = GetComponent<LineRenderer>(); // ���η����� ������Ʈ ����
    }

    public void Call(Vector3 worldPosition)
    {
        if (target.useWorldSpace) // target�� ���� ��ǥ���� �״�� ���
        {
            target.SetPosition(index, worldPosition);
        }
        else
        {
            var localPosition = transform.InverseTransformPoint(worldPosition); // �׷��� �ʴٸ� �������������� ������ ��
            target.SetPosition(index, localPosition); // �ش� ������ �� �� ��ġ �缳��
        }
    }
}
