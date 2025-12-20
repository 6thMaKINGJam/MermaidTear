using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Conversation conversation;

    private bool triggered = false;

//충돌시 무조건 실행되는 메서드 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[DialogueTrigger] Trigger Enter: {other.name} tag={other.tag}");

        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        if (dialogueManager == null)
        {
            Debug.LogError("[DialogueTrigger] dialogueManager가 연결 안 됨!");
            return;
        }
        if (conversation == null)
        {
            Debug.LogError("[DialogueTrigger] conversation(SO)이 연결 안 됨!");
            return;
        }

        dialogueManager.StartDialogue(conversation);
    }
}
