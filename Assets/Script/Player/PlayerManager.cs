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

    public Transform playerTrs => _playerTrs;
    public PlayerStateType playerStateType => _playerStateType;

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
            _flappyBirdGame.F_StartFlappyBird();

        }
        if (Input.GetKeyDown(KeyCode.L))
        { 
            _playerStateType = PlayerStateType.Village;
        
        }
    }

    
}

