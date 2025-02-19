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

    // PlayerManger에서 받아오기 - 포톤 플레이어를 만든 후 실행해야함
    public void F_SettinPlayer(Transform photonPlayer) 
    {
        _player = photonPlayer;
        _playerRb = photonPlayer.GetComponent<Rigidbody2D>();

        // 초기 - 마을 
        _movementAction = F_MovePlayer;

        // 플레이어 움직임 코루틴 시작
        StartCoroutine(IE_PlayerMovenet());
    }

    #region 플레이어 움직임 코루틴 
    
    private IEnumerator IE_PlayerMovenet() 
    {
        while (true)
        {
            if (_movementAction != null)
                _movementAction.Invoke();
            // 매프레임마다 
            yield return null;
        }
    }
    #endregion

    #region 플레이어 movement 동작 메서드 
    
    public void F_NullMoveAction() 
    {
        _movementAction = null;

        // 힘 초기화
        _playerRb.velocity = Vector2.zero;
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

        _playerRb.velocity = new Vector2 (hori, verti) * PlayerManager.Instnace.nowPlayer.PlayerSpeed;
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

    #endregion
}
