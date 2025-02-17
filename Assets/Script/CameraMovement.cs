using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("===Camera===")]
    [SerializeField] private Transform _camera;         // ī�޶� 
    [SerializeField] private Transform _playerTrs;
    [SerializeField] private Vector3 _cameraDestination;   // ī�޶� ��ġ 

    [Header("===Map Edge===")]
    [SerializeField] private Transform _leftDownEdge;   // ���� �Ʒ�
    [SerializeField] private Transform _rightUpEdge;    // ������ ��

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
                // �����϶� -> �÷��̾� ���� 
                case PlayerStateType.Village:
                    F_CheskCameraLimit();
                    _camera.position = _cameraDestination + _cameraZOffset;
                    break;
                // �̴ϰ���1 -> �����忡 ���� 
                case PlayerStateType.MinigameOne:
                    _camera.position = _miniGameOne.position + _cameraZOffset;
                    break;

            }
        }
    }

    private void F_CheskCameraLimit() 
    {
        // x�� y ��ġ�� ���� ����
        float clampedX = Mathf.Clamp(_playerTrs.position.x,
            _leftDownEdge.position.x + halfWidth,
            _rightUpEdge.position.x - halfWidth);

        float clampedY = Mathf.Clamp(_playerTrs.position.y,
            _leftDownEdge.position.y + halfHeight,
            _rightUpEdge.position.y - halfHeight);

        // ���� ī�޶� ��ġ ����
        _cameraDestination = new Vector3(clampedX, clampedY, 0);
    }

}
