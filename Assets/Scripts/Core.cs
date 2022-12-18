using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Core : MonoBehaviour
{
    public int maxHP = 10; // 최대 체력
    private int hp; // 현재 체력

    public UnityEvent<string> OnHpChanged; // HP가 변화되었을 떄 실행될 이벤트
    public UnityEvent OnHit; // 피격당했을 때 이벤트
    public UnityEvent OnDestroy; // HP가 소모되어서 파괴되었을 떄 이벤트

    private static Core instance;

    private static Core Instance // 싱글톤 패턴
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<Core>();
            return instance;
        }
    }

    private void Awake() // 현재 객체를 instance가 참조하도록 처리
    {
        instance = this;
    }

    private void OnEnable() // 활성화되면 hp는 최대값으로 변경, UI 업데이트
    {
        hp = maxHP;
        UpdateUI();
    }

    public void OnTriggerEnter(Collider other) // 몹이 충돌했을 때
    {
        var mob = other.GetComponent<Mob>();
        if (mob != null)
        {
            OnHit?.Invoke(); // OnHit 이벤트에 연결된 함수 실행
            DecreaseHP(1); // HP 1 감소
            mob.Destroy(); // 몹 파괴
        }
    }

    private void DecreaseHP(int amount) // HP 감소 함수
    {
        if (hp <= 0) return;

        hp -= amount;
        if (hp <= 0)
        {
            hp = 0;
            OnDestroy?.Invoke();
        }

        UpdateUI();
    }

    private void UpdateUI() // UI 업데이트 함수
    {
        OnHpChanged?.Invoke($"HP : {hp}");
    }
}
