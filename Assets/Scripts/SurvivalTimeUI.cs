using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivalTimeUI : MonoBehaviour
{
    private float startTime; // 시작 시간 저장

    private TextMeshProUGUI textUI;

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();    
    }

    private void OnEnable()
    {
        startTime = Time.time; // 활성화된 시점의 시간을 시작 시간으로 저장
    }

    private void Update()
    {
        textUI.text = $"Survival Time\n{Time.time - startTime : 0.0}s"; // 시작 시간 이후부터 현재까지의 시간을 UI에 출력
    }
}
