using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Core : MonoBehaviour
{
    public int maxHP = 10; // �ִ� ü��
    private int hp; // ���� ü��

    public UnityEvent<string> OnHpChanged; // HP�� ��ȭ�Ǿ��� �� ����� �̺�Ʈ
    public UnityEvent OnHit; // �ǰݴ����� �� �̺�Ʈ
    public UnityEvent OnDestroy; // HP�� �Ҹ�Ǿ �ı��Ǿ��� �� �̺�Ʈ

    private static Core instance;

    private static Core Instance // �̱��� ����
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<Core>();
            return instance;
        }
    }

    private void Awake() // ���� ��ü�� instance�� �����ϵ��� ó��
    {
        instance = this;
    }

    private void OnEnable() // Ȱ��ȭ�Ǹ� hp�� �ִ밪���� ����, UI ������Ʈ
    {
        hp = maxHP;
        UpdateUI();
    }

    public void OnTriggerEnter(Collider other) // ���� �浹���� ��
    {
        var mob = other.GetComponent<Mob>();
        if (mob != null)
        {
            OnHit?.Invoke(); // OnHit �̺�Ʈ�� ����� �Լ� ����
            DecreaseHP(1); // HP 1 ����
            mob.Destroy(); // �� �ı�
        }
    }

    private void DecreaseHP(int amount) // HP ���� �Լ�
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

    private void UpdateUI() // UI ������Ʈ �Լ�
    {
        OnHpChanged?.Invoke($"HP : {hp}");
    }
}
