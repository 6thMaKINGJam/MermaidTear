using UnityEngine;

public class BubbleProjectile : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed = 5f;
    private Vector2 direction = Vector2.right;

    [Header("LifeTime")]
    [SerializeField] private float lifeTime = 3f;

    [Header("Damage")]
    [SerializeField] private float damage = 1f;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // 시간 지나면 자동 삭제
    }

    // 발사 직후 방향 세팅용
    public void Init(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어 맞으면 데미지 + 즉시 삭제
        if (other.CompareTag("Player"))
        {
            var ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.ReduceHealth(damage);
            }
            Destroy(gameObject);
            return;
        }

    
    }
}

