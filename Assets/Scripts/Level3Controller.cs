using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level3Controller : MonoBehaviour
{
    public DialogueManager dialogueManager;

    [SerializeField]
    private GameObject gameOverPanel;

    [Header("Conversations")]
    public Conversation introConversation;        //  씬 시작 3초 뒤 시작할 첫 대화
    public Conversation mercyEndingConversation;  // (기존 연민 루트용)

    [HideInInspector]
    public bool isGameOver = false;


    private void Start()
    {
        if (dialogueManager == null)
        {
            Debug.LogError("[Level3Controller] dialogueManager가 연결 안 됨!");
            return;
        }

        dialogueManager.OnDialogueEvent += HandleEvent;

        //  씬 시작 3초 뒤 첫 대화 시작
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

    private void HandleEvent(string key)
    {
        if (key == "FIGHT_QUEEN")
        {
            // 전투
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
        SetGameOver();
    }

    private IEnumerator StartMercyEndingNextFrame()
    {
        yield return null; // (선택) 1프레임 뒤
        dialogueManager.StartDialogue(mercyEndingConversation);
    }

    private void OnDestroy()
    {
        if (dialogueManager != null)
            dialogueManager.OnDialogueEvent -= HandleEvent;
    }

    public void SetGameOver()
    {
        isGameOver = true;
        
        Invoke("ShowGameOverPanel", 3f);
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level 1");
    }


}
