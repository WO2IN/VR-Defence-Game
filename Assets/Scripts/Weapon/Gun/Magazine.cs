using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour, IReloadable
{
    public int maxBullets = 20; // �ִ� źâ ����
    public float chargingTime = 2f; // ���� �ð�

    private int currentBullets; // ���� źâ ���� ���� ����

    // Property ����, �Ӽ�(�������) --> getter, setter
    private int CurrentBullets
    {
        get => currentBullets;
        set
        {
            if (value< 0) currentBullets = 0;
            else if (value >= maxBullets) currentBullets = maxBullets;
            else currentBullets = value;

            OnBulletsChanged?.Invoke(currentBullets);
            OnChargingChanged?.Invoke((float)currentBullets / maxBullets);
        }
    }

    public UnityEvent OnReloadStart;
    public UnityEvent OnReloadEnd;

    public UnityEvent<int> OnBulletsChanged;
    public UnityEvent<float> OnChargingChanged;

    private void Start()
    {
        CurrentBullets = maxBullets;
    }

    public bool Use(int amount = 1)
    {
        if (CurrentBullets >= amount)
        {
            CurrentBullets -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    [ContextMenu("Reload")]

    public void StartReload()
    {
        if (currentBullets == maxBullets) return;

        StopAllCoroutines();
        StartCoroutine(ReloadProcess());
    }

    public void StopReload()
    {
        StopAllCoroutines();
    }

    public IEnumerator ReloadProcess()
    {
        OnReloadStart?.Invoke();

        var beginTime = Time.time; // ���� �ð�
        var beginBullets = currentBullets; // ���� ź�� ��
        var enoughPercent = 1f - (float)currentBullets / maxBullets; // �� % ź���� ���Ҵ���
        var enoughChargingTime = chargingTime * enoughPercent; // ���� ź���� �����ϱ� ���� �ð�

        while (true)
        {
            var t = (Time.time - beginTime) / enoughChargingTime; // ������ �� % ����Ǿ����� ����

            if (t > 1f) break; // 100% ���� ������ �ݺ��� ���Ƴ���

            CurrentBullets = (int)Mathf.Lerp(beginBullets, maxBullets, t); // ���� ź�࿡�� �ִ� ź�� �� ���̿� ��ġ�� ���� �����ð��� �̿��� ��������

            yield return null;
        }

        CurrentBullets = maxBullets; // ������ ������ �� �ݺ����� ����Ƿ� �� ��ġ���� �ִ� ź����� �Ǿ�� ��

        OnReloadEnd?.Invoke();
    }
}
