using UnityEngine;

public class CrabBubbleShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform mouthPoint;           // 입 위치
    [SerializeField] private BubbleProjectile bubblePrefab;  // 버블 프리팹

    [Header("Shoot")]
    [SerializeField] private float shootInterval = 1.2f;  // 발사 간격
    [SerializeField] private float startDelay = 0.5f;      // 전투 시작 후 딜레이(선택)

    [Header("Aim")]
    [SerializeField] private bool aimAtPlayer = true;
    [SerializeField] private Transform player; // aimAtPlayer=true면 자동으로 넣을 수도 있음

    private float timer;
    private bool canShoot = false;

    private void Start()
    {
        // 플레이어 자동 찾기(선택)
        if (player == null)
        {
            var p = GameObject.FindWithTag("Player");
            if (p != null) player = p.transform;
        }

        timer = shootInterval - startDelay;
    }

    private void Update()
    {
        if (!canShoot) return;

        timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
            timer = 0f;
            Shoot();
        }
    }

    public void SetShooting(bool on)
    {
        canShoot = on;
        timer = 0f;
    }

    private void Shoot()
    {
        if (bubblePrefab == null || mouthPoint == null) return;

        // 발사 위치는 mouthPoint.position (크랩이 움직여도 입 위치 따라감)
        BubbleProjectile bubble = Instantiate(bubblePrefab, mouthPoint.position, Quaternion.identity);

        Vector2 dir;

        if (aimAtPlayer && player != null)
            dir = (player.position - mouthPoint.position).normalized;  // 플레이어 조준
        else
            dir = Vector2.left; // 고정 방향(예: 왼쪽)으로 발사

        bubble.Init(dir);
    }
}
