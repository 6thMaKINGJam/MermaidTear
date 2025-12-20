using UnityEngine;

public class Level3Controller : MonoBehaviour
{
    public DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager.OnDialogueEvent += HandleEvent;
    }

    private void HandleEvent(string key)
    {
        if (key == "FIGHT_QUEEN")
        {
            Debug.Log("전투 루트 시작!");
            // TODO: 인어여왕과의 싸움 시작(보스 AI 활성화, 전투 UI 켜기 등)
        }
        else if (key == "MERCY_PATH")
        {
            Debug.Log("연민 루트 시작!");
            // 
        }
        else if (key == "SHOW_VISION")
        {
            Debug.Log("환영 컷신!");
            // TODO: VisionPanel 켜고 1~2초 후 끄기
        }
    }
}
