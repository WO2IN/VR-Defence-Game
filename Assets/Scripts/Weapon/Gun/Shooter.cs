using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // 이벤트 라이브러리 추가

public class Shooter : MonoBehaviour
{
    public LayerMask hittableMask; // 인터랙션 대상들을 위한 레이어마스크
    public GameObject hitEffectPrefab; // 광선에 맞았을 떄 필요한 이펙트 오브젝트
    public Transform shootPoint; // 총구 끝 지점

    public float shootDelay = 0.1f; // 총 발사 간격
    public float maxDistance = 100f; // 최대거리

    public UnityEvent<Vector3> OnShootSuccess; // 총 발사 성공 시 이펙트
    public UnityEvent OnShootFail; // 총 발사 실패 시 이펙트

    public Magazine magazine; // Magazine 컴포넌트 참조 변수 선언

    private void Awake()
    {
        magazine = GetComponent<Magazine>();    
    }

    private void Start()
    {
        Stop(); // 모든 동작 멈추기
    }


    public void Stop()
    {
        StopAllCoroutines(); // 모든 코르틴 중지
    }
    public void Play()
    {
        StopAllCoroutines(); // Play() 함수 호출 시 실행중인 모든 코르틴 중지
        StartCoroutine(Process()); // Process() 코르틴 함수 실행
    }

    public IEnumerator Process() // 코르틴 함수
    {
        var wfs = new WaitForSeconds(shootDelay); // 지연시간 객체 생성

        while (true)
        {
            if (magazine.Use()) // 탄창이 있으면 1개 차감
                Shoot(); // 발사
            else // 탄창이 없으면
                OnShootFail?.Invoke(); // 발사 실패 이벤트 함수 호출

            yield return wfs; // shootDelay 만큼 대기
        }
    } 

    private void Shoot()
    {
        // 레이캐스팅 했을 떄 (광선을 쐈을 때) 성공여부에 따라 정보를 전달하여 이벤트 함수 호출
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hitInfo, maxDistance, hittableMask))
        {
            Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.identity); // 레이캐스팅 성공 시 이펙트프리팹 오브젝트 생성

            var hitObject = hitInfo.transform.GetComponent<Hittable>();
            hitObject?.Hit();

            OnShootSuccess?.Invoke(hitInfo.point); // 총에 맞은 대상 오브젝트 위치를 전달하여 OnShootSucess 이벤트 함수 호출
        }
        else
        {
            var hitPoint = shootPoint.position + shootPoint.forward * maxDistance; // 총에 맞지 않았을 때 허공 위치 대략 연산
            OnShootSuccess?.Invoke(hitPoint); // hitPoint와 함께 OnShootSucess 이벤트 함수 호출
        }
    }
}
