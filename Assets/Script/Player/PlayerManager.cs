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
        // ó���� ������ ���� 
        _playerStateType = PlayerStateType.Village;
    }

    private void Update()
    {
        // ##TODO : �ӽ� 
        if (Input.GetKeyDown(KeyCode.K))
        { 
            _playerStateType = PlayerStateType.MinigameOne;

            // player������, camera ������ ��ȯ
            F_ChangeActionByState();

            // �̴ϰ��� ����
            _flappyBirdGame.F_StartFlappyBird();

            // �÷��̾� ��ġ - �̴ϰ��� ��ġ�� 
            F_ChangePlayerPosition(_flappyBirdGame.FluppyPlayerTrs.position);

        }
        if (Input.GetKeyDown(KeyCode.L))
        { 
            _playerStateType = PlayerStateType.Village;

            // player ������, camera ������ ��ȯ
            F_ChangeActionByState();

            // �̴ϰ��� ����
            _flappyBirdGame.F_StopFlappyBird();

            // �÷��̾� ��ġ 0,0,0����
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

