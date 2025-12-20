using UnityEngine;
using System.Collections;

public class Level3Controller : MonoBehaviour
{
    public DialogueManager dialogueManager;


    [Header("Conversations")]
    public Conversation mercyEndingConversation;

    private void Start()
    {
        if (dialogueManager == null)
        {
            Debug.LogError("[Level3Controller] dialogueManager가 연결 안 됨!");
            return;
        }

        dialogueManager.OnDialogueEvent += HandleEvent;
    }

    private void HandleEvent(string key)
    {
        if (key == "FIGHT_QUEEN")
        {
            //전투
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
}
