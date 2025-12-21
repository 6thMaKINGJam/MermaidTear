using System;
using UnityEngine;

[Serializable]
public class DialogueChoice
{
    public string text;        // 버튼에 보여줄 문구
    public int nextIndex = -1; // 선택 후 이동할 대사 인덱스 (-1이면 대화 종료)
    public string eventKey;    // 선택 시 발생시킬 이벤트 키 (예: FIGHT_QUEEN)
}

[Serializable]
public class DialogueLine
{
    public string speaker;

    [TextArea(2, 6)]
    public string text;

    public int nextIndex = -1; // 선택지 없을 때 다음 줄

    public bool hasChoices = false;
    public DialogueChoice choice1;
    public DialogueChoice choice2;

    // (선택) 이 줄에 도달하면 발생시키는 이벤트 (환영 컷신 등)
    public string lineEventKey;
}
