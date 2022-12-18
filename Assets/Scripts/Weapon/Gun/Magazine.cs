using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour, IReloadable
{
    public int maxBullets = 20; // 최대 탄창 개수
    public float chargingTime = 2f; // 충전 시간

    private int currentBullets; // 현재 탄창 개수 저장 변수

    // Property 문법, 속성(멤버변수) --> getter, setter
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

        var beginTime = Time.time; // 시작 시간
        var beginBullets = currentBullets; // 시작 탄약 수
        var enoughPercent = 1f - (float)currentBullets / maxBullets; // 몇 % 탄약이 남았는지
        var enoughChargingTime = chargingTime * enoughPercent; // 남은 탄약을 충전하기 위한 시간

        while (true)
        {
            var t = (Time.time - beginTime) / enoughChargingTime; // 충전이 몇 % 진행되었는지 연선

            if (t > 1f) break; // 100% 충전 끝나면 반복문 벗아나고

            CurrentBullets = (int)Mathf.Lerp(beginBullets, maxBullets, t); // 시작 탄약에서 최대 탄약 수 사이에 위치한 값을 충전시간을 이용해 선형보간

            yield return null;
        }

        CurrentBullets = maxBullets; // 충전이 끝냈을 때 반복문을 벗어나므로 이 위치에서 최대 탄약수가 되어야 함

        OnReloadEnd?.Invoke();
    }
}
