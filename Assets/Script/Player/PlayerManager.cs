using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class PlayerManager : Singleton<PlayerManager>
{

    [Header("===Player===")]
    [SerializeField] private Transform _playerTrs;
    [SerializeField] private PlayerStateType _playerStateType;

    [Header("===Sript===")]
    [SerializeField] private BlockGenerate _flappyBirdGame;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private CameraMovement _cameraMovement;

    public Transform playerTrs => _playerTrs;
    public PlayerStateType playerStateType => _playerStateType;
    public PlayerMovement playerMovement => _playerMovement;

    protected override void Singleton_Awake()
    {
        
    }

    private void Start()
    {
        // 처음은 마을로 설정 
        _playerStateType = PlayerStateType.Village;
    }

    private void Update()
    {
        // ##TODO : 임시 
        if (Input.GetKeyDown(KeyCode.K))
        { 
            _playerStateType = PlayerStateType.MinigameOne;

            // player움직임, camera 움직임 변환
            F_ChangeActionByState();

            // 미니게임 시작
            _flappyBirdGame.F_StartFlappyBird();

            // 플레이어 위치 - 미니게임 위치로 
            F_ChangePlayerPosition(_flappyBirdGame.FluppyPlayerTrs.position);

        }
        if (Input.GetKeyDown(KeyCode.L))
        { 
            _playerStateType = PlayerStateType.Village;

            // player 움직임, camera 움직임 변환
            F_ChangeActionByState();

            // 미니게임 종료
            _flappyBirdGame.F_StopFlappyBird();

            // 플레이어 위치 0,0,0으로
            F_ChangePlayerPosition(new Vector3(0,0,0));
        }
    }

    private void F_ChangeActionByState() 
    {
        _playerMovement.F_UpdatePlayeMonvement(_playerStateType);
        _cameraMovement.F_UpdateCameraMovement(_playerStateType);
    }

    public void F_ChangePlayerPosition(Vector3 potision) 
    {
        _playerTrs.position = potision;
    }
    
}

