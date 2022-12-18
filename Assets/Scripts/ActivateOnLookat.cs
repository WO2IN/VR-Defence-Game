using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnLookat : MonoBehaviour
{
    public new Camera camera; // ���� �޼ҵ尡 ���Ե� ī�޶� Ŭ���� ������
    public Behaviour target; // ������Ʈ�� ���ӿ�����Ʈ Ȱ��ȭ ���θ� �����ϴ� Behaviour ��ü����

    public float thresholdAngle = 30f; // ī�޶� ��ġ���� UI ������Ʈ�� ����
    public float thresholdDuration = 2f; // UI ��� ó�� �ð�

    private bool isLooking = false; // UI ��� ����
    private float showingTime; // UI ��� �ð�

    private void Awake()
    {
        target.enabled = false; // ��Ȱ��ȭ
    }

    private void Update()
    {
        var dir = target.transform.position - camera.transform.position; // ī�޶� target�� �ٶ󺸴� ���� ���
        var angle = Vector3.Angle(camera.transform.forward, dir); // ī�޶� �������� �� �� ���ư��ִ��� ���

        if (angle <= thresholdAngle) // 30�� ���ϸ� ī�޶� �ٶ󺸰� �ִ� ������ �Ǵ�
        {
            if (!isLooking) // ���� �ٶ󺸴� ���¶�� ����ؾ� �� �ð� ����
            {
                isLooking = true;
                showingTime = Time.time + thresholdDuration;
            }
            else // ��� �ٶ󺸰� �ִ� ���¶��
            {
                if (target.enabled && Time.time >= showingTime) // target�� ��Ȱ��ȭ ���� && ��� �ð����� ���� �ð��� ũ�ٸ�
                {
                    target.enabled = true;
                }
            }
        }
        else // 30�� �ʰ��� ī�޶� �ٶ󺸰� ���� ���� ������ �Ǵ�
        {
            if (isLooking) // ���� �ٶ� ���¶�� ��Ȱ��ȭ
            {
                isLooking = false;
                target.enabled = false;
            }
        }
    }
}
