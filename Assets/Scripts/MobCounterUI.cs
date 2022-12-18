using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro �߰�

public class MobCounterUI : MonoBehaviour
{
    private int killCount; // �ı��� ���� ����
    private int spawnCount; // ������ ���� ����

    private TextMeshProUGUI textUI; // UI ��ü ����

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateUI()
    {
        if (!enabled) return; // ��Ȱ��ȭ ���¸� ����

        textUI.text = $"Kill/Alive/Spawn\n{killCount}/{spawnCount - killCount}/{spawnCount}"; // �ؽ�Ʈ ��� (���� ��, ���� ��, ��ü ��)
    }

    private void OnEnable() // Ȱ��ȭ�Ǹ�
    {
        killCount = spawnCount = 0; // �ʱ�ȭ
        UpdateUI(); // UI ������Ʈ
    }

    public void OnSpawn() // �� �����ϸ�
    {
        spawnCount++; // ���� ����
        UpdateUI(); // UI ������Ʈ
    }

    public void OnKill() // �� ������
    {
        killCount++; // ���� ����
        UpdateUI(); // UI ������Ʈ
    }
}
