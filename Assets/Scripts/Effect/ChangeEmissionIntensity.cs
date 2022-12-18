using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmissionIntensity : MonoBehaviour
{
    public float min = 0f; // 컬러 방출 최소값
    public float max = 0f; // 컬러 방출 최대값

    private Renderer target; // Renderer 참조 변수

    private void Awake()
    {
        target = GetComponent<Renderer>(); // 참조
    }

    public void Call(float ratio)
    {
        var intensity = Mathf.Lerp(min, max, ratio); // min ~ max 사이의 값으로 ratio를 이용해서 선형보간-강도 값 연산
        target.material.SetColor("_EmissionColor", target.material.color * intensity); // Renderer에 강도를 이용하여 컬러 재설정
    }
}
