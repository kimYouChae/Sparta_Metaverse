using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    [Header("===Photon View")]
    private PhotonView playerView;

    // PlayerManger���� �޾ƿ��� - ���� �÷��̾ ���� �� �����ؾ���
    public void F_SettinPlayer(Transform photonPlayer) 
    {
        _player = photonPlayer;
        _playerRb = photonPlayer.GetComponent<Rigidbody2D>();
        playerView = photonPlayer.GetComponent<PhotonView>();

        // �ʱ� - ���� 
        _movementAction = F_MovePlayer;

        // �÷��̾� ������ �ڷ�ƾ ����
        StartCoroutine(IE_PlayerMovenet());
    }

    #region �÷��̾� ������ �ڷ�ƾ 
    
    private IEnumerator IE_PlayerMovenet() 
    {
        while (true)
        {
            // ����üũ - ������ �ƴϸ� ������ x
            if (!playerView.IsMine)
                continue;

            if (_movementAction != null)
                _movementAction.Invoke();

            // �������Ӹ��� 
            yield return null;
        }
    }
    #endregion

    #region �÷��̾� movement ���� �޼��� 
    
    public void F_NullMoveAction() 
    {
        _movementAction = null;

        // �� �ʱ�ȭ
        _playerRb.velocity = Vector2.zero;
    }

    // PlayerManger���� ���°� ��ȭ�� �� 1ȸ ����
    public void F_UpdatePlayeMonvement(PlayerStateType type) 
    {
        switch (type)
        {
            // �����϶� -> �߷� x , wasd�� �̵� 
            case PlayerStateType.Village:
                F_OnOffGravity(0);
                _movementAction = F_MovePlayer;
                break;

            // �̴ϰ���1 -> �߷� 0.5 (BlockGenerate��ũ��Ʈ���� ���� ���� �� �ٲ� )
            case PlayerStateType.MinigameOne:
                // F_OnOffGravity(0.5f);
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
        // �����̽� ������ 
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            _playerRb.velocity = Vector2.zero;
            _playerRb.AddForce(Vector2.up * _addForeAmount, ForceMode2D.Force);
        }   
    }

    public void F_OnOffGravity(float gravity) 
    {
        // �÷��̾� �߷��� �ʱ⿡ 0.5 �̿����Ѵ� 
        // 0.5 -> 0
        // 0 -> 0.5
        _playerRb.gravityScale = gravity;
    }

    #endregion
}
