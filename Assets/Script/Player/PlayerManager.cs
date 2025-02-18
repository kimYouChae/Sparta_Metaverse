using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class PlayerManager : Singleton<PlayerManager>
{

    [Header("===Player===")]
    [SerializeField] private Transform _playerTrs;
    [SerializeField] private PlayerStateType _playerStateType;
    [SerializeField] private Player _nowPlayer;

    [Header("===Sript===")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private CameraMovement _cameraMovement;

    // 델리게이트
    private delegate void DEL_HandlePlayerState(PlayerStateType type);
    private DEL_HandlePlayerState del_handlePlayerState;

    public Transform playerTrs => _playerTrs;
    public PlayerStateType playerStateType => _playerStateType;
    public PlayerMovement playerMovement => _playerMovement;

    protected override void Singleton_Awake()
    {
        _nowPlayer = new Player("김유채" , 10 , 5 , 5);
    }

    private void Start()
    {
        // 처음은 마을로 설정 
        _playerStateType = PlayerStateType.Village;

        // 델리게이트에 함수 추가 
        // 1. 플레이어 움직임
        // 2. 카메라 움직임
        // 3. ui 패널 onoff
        del_handlePlayerState += _playerMovement.F_UpdatePlayeMonvement;
        del_handlePlayerState += _cameraMovement.F_UpdateCameraMovement;
        del_handlePlayerState += UiManager.Instnace.F_OnOffPanelByState;

        // 초기 실행
        del_handlePlayerState.Invoke(PlayerStateType.Village);
    }

    private void Update()
    {
        // ##TODO : 임시 
        if (Input.GetKeyDown(KeyCode.K))
        { 
            _playerStateType = PlayerStateType.MinigameOne;

            // 델리게이트 실행 
            del_handlePlayerState.Invoke(_playerStateType);

            // 미니게임 시작
            MiniGameManager.Instnace.fluppyBirdGame.F_StartFlappyBird();

            // 플레이어 위치 - 미니게임 위치로 
            F_ChangePlayerPosition(MiniGameManager.Instnace.FluppyPlayerTrs.position);

        }
    }

    public void F_ChangePlayerPosition(Vector3 potision) 
    {
        _playerTrs.position = potision;
    }

    // FluppyGame - 장애물과 충돌
    public void F_CollisionToBlcok() 
    {
        bool flag = _nowPlayer.F_UpdateHp(-1);

        // 죽으면 ?
        if (!flag)
        {
            _playerStateType = PlayerStateType.Village;

            // 델리게이트 실행 
            del_handlePlayerState.Invoke(_playerStateType);

            // 미니게임 종료
            MiniGameManager.Instnace.fluppyBirdGame.F_StopFlappyBirdCoru();

            // 플레이어 위치 0,0,0으로
            F_ChangePlayerPosition(new Vector3(0, 0, 0));
        }
        else 
        {
            // 안죽으면 -> Fluppy Ui 업데이트 
            MiniGameManager.Instnace.fluidUi.F_UpdateHeartIcon(1);
        }
        
    }
    
}

