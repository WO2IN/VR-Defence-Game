using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmissionIntensity : MonoBehaviour
{
    public float min = 0f; // �÷� ���� �ּҰ�
    public float max = 0f; // �÷� ���� �ִ밪

    private Renderer target; // Renderer ���� ����

    private void Awake()
    {
        target = GetComponent<Renderer>(); // ����
    }

    public void Call(float ratio)
    {
        var intensity = Mathf.Lerp(min, max, ratio); // min ~ max ������ ������ ratio�� �̿��ؼ� ��������-���� �� ����
        target.material.SetColor("_EmissionColor", target.material.color * intensity); // Renderer�� ������ �̿��Ͽ� �÷� �缳��
    }
}
