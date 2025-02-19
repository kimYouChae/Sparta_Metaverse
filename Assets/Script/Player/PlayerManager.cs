using Photon.Pun;
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
    public Player nowPlayer => _nowPlayer;

    protected override void Singleton_Awake()
    {

        // Photon에서 플레이어 생성 시 실행할 델리게이트
        PhotonManager.Instnace.playerCreated += SetPlayer;
    }

    public void SetPlayer() 
    {
        // 포톤에서 생성한 플레이어를 가져오기
        _playerTrs = PhotonManager.Instnace.photonPlayer.transform;

        // 플레이어 생성 
        _nowPlayer = new Player(PhotonNetwork.NickName , 5,5,5,3);

        // movement에 플레이어 주입
        _playerMovement.F_SettinPlayer(_playerTrs);
        // camaeraMove에 플레이어 주입 
        _cameraMovement.F_SettingPlayer(_playerTrs);

        // player타입에 따른 player/camere 동작 
        F_InitPlayer();
    }

    private void F_InitPlayer() 
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

    public void F_ChangePlayerPosition(Vector3 potision) 
    {
        _playerTrs.position = potision;
    }

    // FluppyGame - 장애물과 충돌
    public void F_CollisionToBlcok() 
    {
        bool flag = _nowPlayer.F_CheckHp(-1);

        // 죽으면 ?
        if (!flag)
        {
            // Fluppy Ui에서 팝업
            MiniGameManager.Instnace.fluidUi.F_OnOffDiePopUp(true);

            // 플레이어 움직임 잠시 멈추기 
            _playerMovement.F_NullMoveAction();

            // 미니게임 종료
            MiniGameManager.Instnace.fluppyBirdGame.F_StopFlappyBirdCoru();
        }
        else 
        {
            // 안죽으면 -> Fluppy Ui 업데이트 
            MiniGameManager.Instnace.fluidUi.F_UpdateHeartIcon(1);
        }
    }

    // fluppyGame - 사망 팝업 확인 누를 시 
    public void F_ClickDiePopUp() 
    {
        _playerStateType = PlayerStateType.Village;

        // Fluppy Ui에서 팝업 끄기
        MiniGameManager.Instnace.fluidUi.F_OnOffDiePopUp(false);

        // 플레이어 점수 업데이트
        _nowPlayer.F_UpdatePlayerState( FlappyScore : MiniGameManager.Instnace.fluppyBirdGame.FluppyScore);

        // 델리게이트 실행 
        del_handlePlayerState.Invoke(_playerStateType);

        // 플레이어 위치 0,0,0으로
        F_ChangePlayerPosition(new Vector3(0, 0, 0));

        // 점수보드에 저장 
        GameManager.Instnace.F_SetPlayerScore(_nowPlayer.PlayerName , _nowPlayer.FlappyGameScore);
    }

    // 게임시작 
    public void F_EnterGame() 
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

