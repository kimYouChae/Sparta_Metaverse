using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("===Camera===")]
    [SerializeField] private Transform _camera;         // ī�޶� 
    [SerializeField] private Transform _playerTrs;

    [Header("===Map Edge===")]
    [SerializeField] private Transform _leftDownEdge;   // ���� �Ʒ�
    [SerializeField] private Transform _rightUpEdge;    // ������ ��

    [Header("===MiniGame===")]
    [SerializeField] private Transform _miniGameOne;

    [Header("===Half Camera===")]
    float halfWidth = 8.8f;
    float halfHeight = 5f;
    Vector3 _cameraZOffset = new Vector3(0,0,-10);

    [Header("===Action===")]
    [SerializeField] Action _cameraAction;

    private void Start()
    {
        _playerTrs = PlayerManager.Instnace.playerTrs;

        // �ʱ� - ����
        _cameraAction = F_CheckLimitAndFollow;
    }

    private void LateUpdate()
    {
        if (_camera != null) 
        {
            if(_cameraAction != null)
                _cameraAction.Invoke();
        }
    }

    // PlayerManger���� ���°� ��ȭ�� �� 1ȸ ����
    public void F_UpdateCameraMovement(PlayerStateType type) 
    {
        switch (type)
        {
            // �����϶� -> �÷��̾� ���� 
            case PlayerStateType.Village:
                _cameraAction = F_CheckLimitAndFollow;
                break;
            // �̴ϰ���1 -> �����忡 ���� 
            case PlayerStateType.MinigameOne:
                _cameraAction = F_MiniGame1Camera;
                break;
        }
    }

    private void F_CheckLimitAndFollow() 
    {
        // x�� y ��ġ�� ���� ����
        float clampedX = Mathf.Clamp(_playerTrs.position.x,
            _leftDownEdge.position.x + halfWidth,
            _rightUpEdge.position.x - halfWidth);

        float clampedY = Mathf.Clamp(_playerTrs.position.y,
            _leftDownEdge.position.y + halfHeight,
            _rightUpEdge.position.y - halfHeight);

        // ���� ī�޶� ��ġ ����
        _camera.position = new Vector3(clampedX, clampedY, 0) + _cameraZOffset;
    }

    private void F_MiniGame1Camera() 
    {
        _camera.position = _miniGameOne.position + _cameraZOffset;
    }
}
