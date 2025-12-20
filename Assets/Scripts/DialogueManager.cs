using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject dialoguePanel;
    public GameObject choicesPanel;

    [Header("Texts")]
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    [Header("Choice Buttons")]
    public Button choiceButton1;
    public Button choiceButton2;

    public TMP_Text choiceText1;
    public TMP_Text choiceText2;

    // ===== 여기부터 "로직용 변수" 추가 =====
    private Conversation conversation;
    private int index = 0;
    private bool isActive = false;

    // 선택 결과/라인 이벤트를 밖으로 알려주는 통로 (Level3Controller가 받음)
    public event Action<string> OnDialogueEvent;

    private void Start()
    {
        // 시작 시 UI 꺼두기
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (choicesPanel != null) choicesPanel.SetActive(false);

        // 버튼 클릭 연결 (Inspector에서 OnClick 안 건드려도 됨)
        if (choiceButton1 != null) choiceButton1.onClick.AddListener(() => Choose(1));
        if (choiceButton2 != null) choiceButton2.onClick.AddListener(() => Choose(2));
    }

    private void Update()
    {
        if (!isActive) return;

        // 선택지 떠있을 땐 Space/클릭으로 넘기기 막기
        if (choicesPanel != null && choicesPanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Next();
        }
    }

    // 트리거가 호출할 시작 함수
    public void StartDialogue(Conversation conv)
    {
        conversation = conv;
        index = 0;
        isActive = true;

        dialoguePanel.SetActive(true);
        ShowLine();
    }

    private void ShowLine()
    {
        if (conversation == null || conversation.lines == null || conversation.lines.Length == 0)
        {
            Debug.LogError("Conversation 데이터가 비어있어!");
            EndDialogue();
            return;
        }

        var line = conversation.lines[index];

        nameText.text = line.speaker;
        dialogueText.text = line.text;

        // (선택) 이 줄 도착 이벤트(환영 컷신 등)
        if (!string.IsNullOrEmpty(line.lineEventKey))
        {
            OnDialogueEvent?.Invoke(line.lineEventKey);
        }

        if (line.hasChoices)
        {
            choicesPanel.SetActive(true);
            choiceText1.text = line.choice1.text;
            choiceText2.text = line.choice2.text;
        }
        else
        {
            choicesPanel.SetActive(false);
        }
    }

    private void Next()
    {
        var line = conversation.lines[index];

        if (line.hasChoices) return;

        if (line.nextIndex < 0)
        {
            EndDialogue();
            return;
        }

        index = line.nextIndex;
        ShowLine();
    }

    private void Choose(int n)
    {
        var line = conversation.lines[index];
        var choice = (n == 1) ? line.choice1 : line.choice2;

        // 선택 결과 이벤트 (전투/연민 루트 등)
        if (!string.IsNullOrEmpty(choice.eventKey))
        {
            OnDialogueEvent?.Invoke(choice.eventKey);
        }

        choicesPanel.SetActive(false);

        if (choice.nextIndex < 0)
        {
            EndDialogue();
            return;
        }

        index = choice.nextIndex;
        ShowLine();
    }

    private void EndDialogue()
    {
        isActive = false;

        if (choicesPanel != null) choicesPanel.SetActive(false);
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }
}
