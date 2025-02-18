using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("===Player===")]
    [SerializeField] Transform _player;
    [Header("===Component===")]
    [SerializeField] private Rigidbody2D _playerRb;

    [Header("===Action===")]
    [SerializeField] Action _movementAction;

    [Header("===State===")]
    [SerializeField] private float _gravityForece = 0.5f;
    [SerializeField] private float _addForeAmount = 100f;

    void Start()
    {
        _player = PlayerManager.Instnace.playerTrs;
        _playerRb = _player.GetComponent<Rigidbody2D>();

        // 초기 - 마을 
        _movementAction = F_MovePlayer;
    }

    void Update()
    {
        if (_movementAction != null)
            _movementAction.Invoke();
    }

    // PlayerManger에서 상태가 변화될 때 1회 실행
    public void F_UpdatePlayeMonvement(PlayerStateType type) 
    {
        switch (type)
        {
            // 마을일때 -> 중력 x , wasd로 이동 
            case PlayerStateType.Village:
                F_OnOffGravity();
                _movementAction = F_MovePlayer;
                break;

            // 미니게임1 -> 중력 0, addforce
            case PlayerStateType.MinigameOne:
                // F_OnOffGravity();
                _movementAction = F_MiniGameMovement;
                break;
        }
    }

    private void F_MovePlayer() 
    {
        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");

        //this.transform.position += new Vector3(hori, verti) * Time.deltaTime * 3f;

        // ##TODO : 플레이어 속도로 바꿔야함 
        _playerRb.velocity = new Vector2 (hori, verti) * 3f;
    }

    private void F_MiniGameMovement() 
    {
        // 스페이스 누르면 
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            _playerRb.velocity = Vector2.zero;
            _playerRb.AddForce(Vector2.up * _addForeAmount, ForceMode2D.Force);
        }   
    }

    public void F_OnOffGravity() 
    {
        // 플레이어 중력은 초기에 0.5 이여야한다 
        // 0.5 -> 0
        // 0 -> 0.5
        _playerRb.gravityScale = _playerRb.gravityScale == _gravityForece ? 0 : _gravityForece;
    }
}
