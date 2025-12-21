using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceDir : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    public enum FaceDir
    {
        Up,
        Down,
        Left,
        Right,
    }

    public FaceDir GetPlayerFaceDir()
    {
        switch (_playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name)
        {
            case "PlayerIdleUp": case "PlayerWalkUp": return FaceDir.Up;
            case "PlayerIdleDown": case "PlayerWalkDown": return FaceDir.Down;
            case "PlayerIdleLeft": case "PlayerWalkLeft": return FaceDir.Left;
            case "PlayerIdleRight": case "PlayerWalkRight": return FaceDir.Right;
            default: return FaceDir.Up;
        }
    }
}
