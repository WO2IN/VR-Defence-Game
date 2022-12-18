using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 추가

public class MobCounterUI : MonoBehaviour
{
    private int killCount; // 파괴딘 몹의 개수
    private int spawnCount; // 생성된 몹의 개수

    private TextMeshProUGUI textUI; // UI 객체 변수

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateUI()
    {
        if (!enabled) return; // 비활성화 상태면 중지

        textUI.text = $"Kill/Alive/Spawn\n{killCount}/{spawnCount - killCount}/{spawnCount}"; // 텍스트 출력 (죽은 몹, 생존 몹, 전체 몹)
    }

    private void OnEnable() // 활성화되면
    {
        killCount = spawnCount = 0; // 초기화
        UpdateUI(); // UI 업데이트
    }

    public void OnSpawn() // 몹 생성하면
    {
        spawnCount++; // 개수 증가
        UpdateUI(); // UI 업데이트
    }

    public void OnKill() // 몹 죽으면
    {
        killCount++; // 개수 증가
        UpdateUI(); // UI 업데이트
    }
}
