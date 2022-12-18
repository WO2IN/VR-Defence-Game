using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinePosition : MonoBehaviour
{
    public int index; // 라인의 인덱스

    private LineRenderer target; // 라인렌더러 컴포넌트 참조 변수

    private void Awake()
    {
        target = GetComponent<LineRenderer>(); // 라인렌더러 컴포넌트 참조
    }

    public void Call(Vector3 worldPosition)
    {
        if (target.useWorldSpace) // target이 월드 좌표계라면 그대로 사용
        {
            target.SetPosition(index, worldPosition);
        }
        else
        {
            var localPosition = transform.InverseTransformPoint(worldPosition); // 그렇지 않다면 로컬포지션으로 변경한 후
            target.SetPosition(index, localPosition); // 해당 라인의 각 점 위치 재설정
        }
    }
}
