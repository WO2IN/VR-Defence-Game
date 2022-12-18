using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class ReturnToTarget : MonoBehaviour
{
    public Transform target; // 거치대 위치

    public float duration = 1f; // 거치대로 총이 이동하는 시간
    public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f); // 부드럽게 이동

    public UnityEvent OnCompleted; // 이동 완료했을 때 실행하는 함수 연결

    public void Call()
    {
        if (!gameObject.activeInHierarchy) return; // 게임 오브젝트가 비활성화 상태라면 리턴

        StopAllCoroutines(); // 게임 오브젝트가 활성화 상태라면 모든 코루틴 함수 중지
        StartCoroutine(Process()); // Process() 코루틴 함수 호출
    }

    public IEnumerator Process()
    {
        if (target == null) yield break; // 목표 위치가 비어있다면 코루틴 함수 중지

        var beginTime = Time.time; // 반복 시작 시간

        while (true)
        {
            var t = (Time.time - beginTime) / duration; // 전체 1초 중 얼마나 진행되었는지(%) 연산

            if (t >= 1f) break; // 100% 진행되었다면 반복문 벗어남

            t = curve.Evaluate(t); // 몇 % 진행되었는지를 curve 값으로 변환

            transform.position = Vector3.Lerp(transform.position, target.position, t); // 위치 정보를 변경(시작에서 목표 중 어느 위치인지)

            yield return null;
        }

        transform.position = target.position; // t가 100% 진행되었다면 현재 오브젝트를 목표 위치로 수정

        OnCompleted?.Invoke(); // OnCompleted 이벤트에 연결된 함수 실행 (재충전)
    }
}
