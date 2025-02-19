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

    // ��������Ʈ
    private delegate void DEL_HandlePlayerState(PlayerStateType type);
    private DEL_HandlePlayerState del_handlePlayerState;

    public Transform playerTrs => _playerTrs;
    public PlayerStateType playerStateType => _playerStateType;
    public PlayerMovement playerMovement => _playerMovement;
    public Player nowPlayer => _nowPlayer;

    protected override void Singleton_Awake()
    {

        // Photon���� �÷��̾� ���� �� ������ ��������Ʈ
        PhotonManager.Instnace.playerCreated += SetPlayer;
    }

    public void SetPlayer() 
    {
        // ���濡�� ������ �÷��̾ ��������
        _playerTrs = PhotonManager.Instnace.photonPlayer.transform;

        // �÷��̾� ���� 
        _nowPlayer = new Player(PhotonNetwork.NickName , 5,5,5,3);

        // movement�� �÷��̾� ����
        _playerMovement.F_SettinPlayer(_playerTrs);
        // camaeraMove�� �÷��̾� ���� 
        _cameraMovement.F_SettingPlayer(_playerTrs);

        // playerŸ�Կ� ���� player/camere ���� 
        F_InitPlayer();
    }

    private void F_InitPlayer() 
    {
        // ó���� ������ ���� 
        _playerStateType = PlayerStateType.Village;

        // ��������Ʈ�� �Լ� �߰� 
        // 1. �÷��̾� ������
        // 2. ī�޶� ������
        // 3. ui �г� onoff
        del_handlePlayerState += _playerMovement.F_UpdatePlayeMonvement;
        del_handlePlayerState += _cameraMovement.F_UpdateCameraMovement;
        del_handlePlayerState += UiManager.Instnace.F_OnOffPanelByState;

        // �ʱ� ����
        del_handlePlayerState.Invoke(PlayerStateType.Village);
    }

    public void F_ChangePlayerPosition(Vector3 potision) 
    {
        _playerTrs.position = potision;
    }

    // FluppyGame - ��ֹ��� �浹
    public void F_CollisionToBlcok() 
    {
        bool flag = _nowPlayer.F_CheckHp(-1);

        // ������ ?
        if (!flag)
        {
            // Fluppy Ui���� �˾�
            MiniGameManager.Instnace.fluidUi.F_OnOffDiePopUp(true);

            // �÷��̾� ������ ��� ���߱� 
            _playerMovement.F_NullMoveAction();

            // �̴ϰ��� ����
            MiniGameManager.Instnace.fluppyBirdGame.F_StopFlappyBirdCoru();
        }
        else 
        {
            // �������� -> Fluppy Ui ������Ʈ 
            MiniGameManager.Instnace.fluidUi.F_UpdateHeartIcon(1);
        }
    }

    // fluppyGame - ��� �˾� Ȯ�� ���� �� 
    public void F_ClickDiePopUp() 
    {
        _playerStateType = PlayerStateType.Village;

        // Fluppy Ui���� �˾� ����
        MiniGameManager.Instnace.fluidUi.F_OnOffDiePopUp(false);

        // �÷��̾� ���� ������Ʈ
        _nowPlayer.F_UpdatePlayerState( FlappyScore : MiniGameManager.Instnace.fluppyBirdGame.FluppyScore);

        // ��������Ʈ ���� 
        del_handlePlayerState.Invoke(_playerStateType);

        // �÷��̾� ��ġ 0,0,0����
        F_ChangePlayerPosition(new Vector3(0, 0, 0));

        // �������忡 ���� 
        GameManager.Instnace.F_SetPlayerScore(_nowPlayer.PlayerName , _nowPlayer.FlappyGameScore);
    }

    // ���ӽ��� 
    public void F_EnterGame() 
    {
        _playerStateType = PlayerStateType.MinigameOne;

        // ��������Ʈ ���� 
        del_handlePlayerState.Invoke(_playerStateType);

        // �̴ϰ��� ����
        MiniGameManager.Instnace.fluppyBirdGame.F_StartFlappyBird();

        // �÷��̾� ��ġ - �̴ϰ��� ��ġ�� 
        F_ChangePlayerPosition(MiniGameManager.Instnace.FluppyPlayerTrs.position);
    }
}

