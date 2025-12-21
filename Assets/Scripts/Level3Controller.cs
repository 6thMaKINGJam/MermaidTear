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

    [Header("Health (for GameOver)")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EnemyHealth crabHealth;

    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Conversations")]
    public Conversation introConversation;
    public Conversation mercyEndingConversation;

    private bool gameOverHandled = false;

    private void Start()
    {
        if (dialogueManager == null)
        {
            Debug.LogError("[Level3Controller] dialogueManager가 연결 안 됨!");
            return;
        }

        // 초기 상태
        if (crabRoot != null) crabRoot.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

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

            if (crabRoot != null) crabRoot.SetActive(true);
            if (mermaid0 != null) mermaid0.SetActive(false);

            crabShooter?.SetShooting(true);  
        }
        else if (key == "MERCY_PATH")
        {
            Debug.Log("연민 루트 시작!");

            if (mercyEndingConversation == null)
            {
                Debug.LogError("[Level3Controller] mercyEndingConversation이 연결 안 됨!");
                return;
            }

            StartCoroutine(StartMercyEndingNextFrame());
        }
    }

    private IEnumerator StartMercyEndingNextFrame()
    {
        yield return null;
        dialogueManager.StartDialogue(mercyEndingConversation);
    }

    // =====================
    // Health 기반 GameOver
    // =====================
    private void HandlePlayerDead()
    {
        if (gameOverHandled) return;
        gameOverHandled = true;

        Debug.Log("[GameOver] Player died");
        ShowGameOverPanel();
    }

    private void HandleCrabDead()
    {
        if (gameOverHandled) return;
        gameOverHandled = true;

        Debug.Log("[GameOver] Crab died");
        ShowGameOverPanel();
    }

    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
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
