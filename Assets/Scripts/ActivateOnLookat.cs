using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnLookat : MonoBehaviour
{
    public new Camera camera; // 가상 메소드가 포함된 카메라 클래스 재정의
    public Behaviour target; // 컴포넌트와 게임오브젝트 활성화 여부를 리턴하는 Behaviour 객체변수

    public float thresholdAngle = 30f; // 카메라 위치에서 UI 오브젝트의 각도
    public float thresholdDuration = 2f; // UI 출력 처리 시간

    private bool isLooking = false; // UI 출력 여부
    private float showingTime; // UI 출력 시간

    private void Awake()
    {
        target.enabled = false; // 비활성화
    }

    private void Update()
    {
        var dir = target.transform.position - camera.transform.position; // 카메라가 target을 바라보는 방향 계산
        var angle = Vector3.Angle(camera.transform.forward, dir); // 카메라를 기준으로 몇 도 돌아가있는지 계산

        if (angle <= thresholdAngle) // 30도 이하면 카메라를 바라보고 있는 것으로 판단
        {
            if (!isLooking) // 새로 바라보는 상태라면 출력해야 할 시간 연산
            {
                isLooking = true;
                showingTime = Time.time + thresholdDuration;
            }
            else // 계속 바라보고 있는 상태라면
            {
                if (target.enabled && Time.time >= showingTime) // target이 비활성화 상태 && 출력 시간보다 현재 시간이 크다면
                {
                    target.enabled = true;
                }
            }
        }
        else // 30도 초과면 카메라를 바라보고 있지 않은 것으로 판단
        {
            if (isLooking) // 만일 바라본 상태라면 비활성화
            {
                isLooking = false;
                target.enabled = false;
            }
        }
    }
}
