using UnityEngine;

public class MermaidMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2.5f;        // 이동 속도
    [SerializeField] private float arriveDistance = 0.1f;   // 목표점 도착 판정 거리(너무 작으면 튕김)

    [Header("Move Area (World Space)")]
    [SerializeField] private Vector2 minPos; // 이동 범위 좌하단 (월드 좌표)
    [SerializeField] private Vector2 maxPos; // 이동 범위 우상단 (월드 좌표)

    [Header("Pause")]
    [SerializeField] private float minPause = 0.0f;  // 목표 도착 후 멈춤 최소시간
    [SerializeField] private float maxPause = 0.4f;  // 목표 도착 후 멈춤 최대시간

    private Vector3 targetPos;     // 현재 이동 목표 위치
    private EnemyHealth health;    // 체력 0이면 이동 멈추게

    private float pauseTimer = 0f; // 멈춤 시간 카운트

    private void Start()
    {
        // 같은 적(크랩/인어)이 EnemyHealth를 쓰고 있으니 가져옴
        health = GetComponent<EnemyHealth>();

        // 시작할 때 목표를 하나 뽑아놓고 이동 시작
        PickRandomTarget();
    }

    private void Update()
    {
        //  체력이 0 이하이면 더 이상 이동하지 않게(기존 EnemyMove랑 동일한 정책)
        if (health != null && health.CurrentHealth <= 0) return;

        // 도착 후 잠깐 멈추게 하고 싶을 때(자연스러움)
        if (pauseTimer > 0f)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }

        // 1) 목표 지점으로 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        // 2) 목표 지점에 충분히 가까워졌다면 다음 목표 뽑기
        if (Vector3.Distance(transform.position, targetPos) <= arriveDistance)
        {
            // 도착 후 랜덤하게 잠깐 멈춤(선택)
            pauseTimer = Random.Range(minPause, maxPause);

            PickRandomTarget();
        }
    }

    private void PickRandomTarget()
    {
        // minPos~maxPos 범위 안에서 랜덤 좌표를 뽑음
        float x = Random.Range(minPos.x, maxPos.x);
        float y = Random.Range(minPos.y, maxPos.y);

        // z는 현재 z 유지(2D에서 레이어 꼬임 방지)
        targetPos = new Vector3(x, y, transform.position.z);
    }
}

