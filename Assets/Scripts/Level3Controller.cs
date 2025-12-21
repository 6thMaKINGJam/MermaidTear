using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level3Controller : MonoBehaviour
{
    public DialogueManager dialogueManager;

    [SerializeField] private CrabBubbleShooter crabShooter;

    [Header("Scene Objects (Toggle)")]
    [SerializeField] private GameObject crabRoot;
    [SerializeField] private GameObject mermaid0;

    [Header("Health (for Result Panels)")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EnemyHealth crabHealth;

    [Header("UI Panels")]
    [SerializeField] private GameObject GameOverPanel;   // 전투 패배(플레이어 사망)
    [SerializeField] private GameObject Ending1Panel;    // 전투 승리(크랩 사망)
    [SerializeField] private GameObject Ending2Panel;    // 연민 엔딩(추가 대화 종료 후)

    [Header("Conversations")]
    public Conversation introConversation;
    public Conversation mercyEndingConversation;

    private bool resultHandled = false;
    private bool isFightRoute = false;
    private bool isMercyRoute = false;

    private void Start()
    {
        if (dialogueManager == null)
        {
            Debug.LogError("[Level3Controller] dialogueManager가 연결 안 됨!");
            return;
        }

        // 초기 상태
        if (crabRoot != null) crabRoot.SetActive(false);

        if (GameOverPanel != null) GameOverPanel.SetActive(false);
        if (Ending1Panel != null) Ending1Panel.SetActive(false);
        if (Ending2Panel != null) Ending2Panel.SetActive(false);

        // 🔹 대화 이벤트
        dialogueManager.OnDialogueEvent += HandleEvent;

        // 🔹 체력 이벤트
        if (playerHealth != null) PlayerHealth.OnDead += HandlePlayerDead;
        else Debug.LogError("[Level3Controller] playerHealth 연결 안 됨!");

        if (crabHealth != null) crabHealth.OnDead += HandleCrabDead;
        else Debug.LogError("[Level3Controller] crabHealth 연결 안 됨!");

        // 🔹 씬 시작 대화
        StartCoroutine(StartIntroAfterDelay(3f));
    }

    private IEnumerator StartIntroAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (introConversation == null)
        {
            Debug.LogError("[Level3Controller] introConversation이 연결 안 됨!");
            yield break;
        }

        dialogueManager.StartDialogue(introConversation);
    }

    // =====================
    // Dialogue Event 처리
    // =====================
    private void HandleEvent(string key)
    {
        if (key == "FIGHT_QUEEN")
        {
            Debug.Log("전투 루트 시작!");

            // ✅ 루트 상태
            isFightRoute = true;
            isMercyRoute = false;
            resultHandled = false;

            // 씬 오브젝트 토글
            if (crabRoot != null) crabRoot.SetActive(true);
            if (mermaid0 != null) mermaid0.SetActive(false);

            // 전투 시작(발사 시작)
            crabShooter?.SetShooting(true);
        }
        else if (key == "MERCY_PATH")
        {
            Debug.Log("연민 루트 시작!");

            // ✅ 루트 상태
            isFightRoute = false;
            isMercyRoute = true;
            resultHandled = false;

            if (mercyEndingConversation == null)
            {
                Debug.LogError("[Level3Controller] mercyEndingConversation이 연결 안 됨!");
                return;
            }

            // ✅ 기존 기능 유지: 다음 프레임에 추가 대화 시작
            StartCoroutine(StartMercyEndingNextFrame());
        }
        else if (key == "MERCY_END")
        {
            // ✅ mercyEndingConversation 마지막에 이벤트 키 MERCY_END 넣으면
            //    "추가 대화가 끝난 직후" 여기로 들어옴
            Debug.Log("연민 엔딩 대화 종료 → Ending2Panel 표시");
            ShowEnding2Panel();
        }
    }

    private IEnumerator StartMercyEndingNextFrame()
    {
        yield return null;
        dialogueManager.StartDialogue(mercyEndingConversation);
    }

    // =====================
    // Health 기반 결과 패널
    // =====================
    private void HandlePlayerDead()
    {
        // ✅ 전투 루트에서만 패배 패널
        if (!isFightRoute) return;

        if (resultHandled) return;
        resultHandled = true;

        Debug.Log("[Result] Player died (Fight) → GameOverPanel");
        ShowGameOverPanel();
    }

    private void HandleCrabDead()
    {
        // ✅ 전투 루트에서만 승리 패널
        if (!isFightRoute) return;

        if (resultHandled) return;
        resultHandled = true;

        Debug.Log("[Result] Crab died (Fight) → Ending1Panel");
        ShowEnding1Panel();
    }

    private void ShowGameOverPanel()
    {
        if (GameOverPanel != null)
            GameOverPanel.SetActive(true);
        else
            Debug.LogWarning("[Level3Controller] GameOverPanel이 연결 안 됨!");
    }

    private void ShowEnding1Panel()
    {
        if (Ending1Panel != null)
            Ending1Panel.SetActive(true);
        else
            Debug.LogWarning("[Level3Controller] Ending1Panel이 연결 안 됨!");
    }

    private void ShowEnding2Panel()
    {
        if (Ending2Panel != null)
            Ending2Panel.SetActive(true);
        else
            Debug.LogWarning("[Level3Controller] Ending2Panel이 연결 안 됨!");
    }

    private void OnDestroy()
    {
        if (dialogueManager != null)
            dialogueManager.OnDialogueEvent -= HandleEvent;

        if (playerHealth != null)
            PlayerHealth.OnDead -= HandlePlayerDead;

        if (crabHealth != null)
            crabHealth.OnDead -= HandleCrabDead;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level 3");
    }
}

