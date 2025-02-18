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

    protected override void Singleton_Awake()
    {
        _nowPlayer = new Player("����ä" , 10 , 5 , 5);
    }

    private void Start()
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

    private void Update()
    {
        // ##TODO : �ӽ� 
        if (Input.GetKeyDown(KeyCode.K))
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

    public void F_ChangePlayerPosition(Vector3 potision) 
    {
        _playerTrs.position = potision;
    }

    // FluppyGame - ��ֹ��� �浹
    public void F_CollisionToBlcok() 
    {
        bool flag = _nowPlayer.F_UpdateHp(-1);

        // ������ ?
        if (!flag)
        {
            _playerStateType = PlayerStateType.Village;

            // ��������Ʈ ���� 
            del_handlePlayerState.Invoke(_playerStateType);

            // �̴ϰ��� ����
            MiniGameManager.Instnace.fluppyBirdGame.F_StopFlappyBirdCoru();

            // �÷��̾� ��ġ 0,0,0����
            F_ChangePlayerPosition(new Vector3(0, 0, 0));
        }
        else 
        {
            // �������� -> Fluppy Ui ������Ʈ 
            MiniGameManager.Instnace.fluidUi.F_UpdateHeartIcon(1);
        }
        
    }
    
}

