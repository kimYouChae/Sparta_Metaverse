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
            MiniGameManager.Instnace.fluppyBirdGame.F_StartFlappyBird();

            // 플레이어 위치 - 미니게임 위치로 
            F_ChangePlayerPosition(MiniGameManager.Instnace.FluppyPlayerTrs.position);

        }
        if (Input.GetKeyDown(KeyCode.L))
        { 
            _playerStateType = PlayerStateType.Village;

            // player 움직임, camera 움직임 변환
            F_ChangeActionByState();

            // 미니게임 종료
            MiniGameManager.Instnace.fluppyBirdGame.F_StopFlappyBird();

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

    // FluppyGame - 장애물과 충돌
    public void F_CollisionToBlcok() 
    {
        bool flag = _nowPlayer.F_UpdateHp(-1);

        // 죽으면 ?
        if (!flag)
        {
            // 게임 끝내고
            // 점수저장후
            // 마을로 돌아오기        
        }
        else 
        {
            // 안죽으면 -> Fluppy Ui 업데이트 
            MiniGameManager.Instnace.fluidUi.F_UpdateHeartIcon(1);
        }
        
    }
    
}

