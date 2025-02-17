using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("===Camera===")]
    [SerializeField] private Transform _camera;         // 카메라 
    [SerializeField] private Transform _playerTrs;
    [SerializeField] private Vector3 _cameraDestination;   // 카메라 위치 

    [Header("===Map Edge===")]
    [SerializeField] private Transform _leftDownEdge;   // 왼쪽 아래
    [SerializeField] private Transform _rightUpEdge;    // 오른쪽 위

    [Header("===MiniGame===")]
    [SerializeField] private Transform _miniGameOne;

    [Header("===Half Camera===")]
    float halfWidth = 8.8f;
    float halfHeight = 5f;
    Vector3 _cameraZOffset = new Vector3(0,0,-10);

    private void Start()
    {
        _playerTrs = PlayerManager.Instnace.playerTrs;
    }

    private void LateUpdate()
    {
        if (_camera != null) 
        {
            switch (PlayerManager.Instnace.playerStateType) 
            {
                // 마을일때 -> 플레이어 따라 
                case PlayerStateType.Village:
                    F_CheskCameraLimit();
                    _camera.position = _cameraDestination + _cameraZOffset;
                    break;
                // 미니게임1 -> 게임장에 고정 
                case PlayerStateType.MinigameOne:
                    _camera.position = _miniGameOne.position + _cameraZOffset;
                    break;

            }
        }
    }

    private void F_CheskCameraLimit() 
    {
        // x와 y 위치를 각각 제한
        float clampedX = Mathf.Clamp(_playerTrs.position.x,
            _leftDownEdge.position.x + halfWidth,
            _rightUpEdge.position.x - halfWidth);

        float clampedY = Mathf.Clamp(_playerTrs.position.y,
            _leftDownEdge.position.y + halfHeight,
            _rightUpEdge.position.y - halfHeight);

        // 최종 카메라 위치 설정
        _cameraDestination = new Vector3(clampedX, clampedY, 0);
    }

}
