using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("===Camera===")]
    [SerializeField] private Transform _camera;         // 카메라 
    [SerializeField] private Transform _playerTrs;

    [Header("===Map Edge===")]
    [SerializeField] private Transform _leftDownEdge;   // 왼쪽 아래
    [SerializeField] private Transform _rightUpEdge;    // 오른쪽 위

    [Header("===MiniGame===")]
    [SerializeField] private Transform _miniGameOne;

    [Header("===Half Camera===")]
    float halfWidth = 8.8f;
    float halfHeight = 5f;
    Vector3 _cameraZOffset = new Vector3(0,0,-10);

    [Header("===Action===")]
    [SerializeField] Action _cameraAction;

    // PlayerManger에서 받아오기 - 포톤 플레이어를 만든 후 실행해야함
    public void F_SettingPlayer(Transform photonPlayer) 
    {
        _playerTrs = photonPlayer;

        // 초기 - 마을
        _cameraAction = F_CheckLimitAndFollow;

        // 카메라 동작 코루틴 실행
        StartCoroutine(IE_CameraMove());
    }

    #region 카메라 움직임 코루틴 
    
    private IEnumerator IE_CameraMove() 
    {
        while (true) 
        {
            if (_camera != null)
            {
                if (_cameraAction != null)
                    _cameraAction.Invoke();
            }

            // 매프레임마다 
            yield return null;
        }
    }
    #endregion

    #region 카메라 movement 동작 메서드 
    
    // PlayerManger에서 상태가 변화될 때 1회 실행
    public void F_UpdateCameraMovement(PlayerStateType type) 
    {
        switch (type)
        {
            // 마을일때 -> 플레이어 따라 
            case PlayerStateType.Village:
                _cameraAction = F_CheckLimitAndFollow;
                break;
            // 미니게임1 -> 게임장에 고정 
            case PlayerStateType.MinigameOne:
                _cameraAction = F_MiniGame1Camera;
                break;
        }
    }

    private void F_CheckLimitAndFollow() 
    {
        // x와 y 위치를 각각 제한
        float clampedX = Mathf.Clamp(_playerTrs.position.x,
            _leftDownEdge.position.x + halfWidth,
            _rightUpEdge.position.x - halfWidth);

        float clampedY = Mathf.Clamp(_playerTrs.position.y,
            _leftDownEdge.position.y + halfHeight,
            _rightUpEdge.position.y - halfHeight);

        // 최종 카메라 위치 설정
        _camera.position = new Vector3(clampedX, clampedY, 0) + _cameraZOffset;
    }

    private void F_MiniGame1Camera() 
    {
        _camera.position = _miniGameOne.position + _cameraZOffset;
    }

    #endregion
}
