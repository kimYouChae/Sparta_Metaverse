using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTypes
{

}

#region ENUM
public enum PlayerStateType
{
    Village,            // 마을
    MinigameOne,        // 미니게임1
    MinigameTwo         // 미니게임2

}

enum BlockType
{
    Long, Short
}

#endregion

#region Player클래스

[System.Serializable]
public class Player
{
    // 기본스탯
    [SerializeField] private string _playerName;
    [SerializeField] private float _playerHP;
    [SerializeField] private float _playerMaxHp;
    [SerializeField] private float _playerAttack;
    [SerializeField] private float _playerSpeed;

    // 점수 
    [SerializeField] private int _flappyGameScore;

    // 프로퍼티
    public string PlayerName { get => _playerName; set => _playerName = value; }
    public float PlayerHP { get => _playerHP; set => _playerHP = value; }
    public float PlayerAttack { get => _playerAttack; set => _playerAttack = value; }
    public float PlayerSpeed { get => _playerSpeed; set => _playerSpeed = value; }
    public int FlappyGameScore { get => _flappyGameScore; set => _flappyGameScore = value; }
    public float PlayerMaxHp { get => _playerMaxHp; set => _playerMaxHp = value; }

    // 생성자 
    public Player(string name, float hp, float att, float df , float speed )
    {
        this._playerName = name;
        this._playerHP = hp;
        this._playerMaxHp = _playerHP;
        this._playerAttack = att;
        this._playerSpeed = df;
        this._playerSpeed = speed;
        this._flappyGameScore = 0;
    }

    // 플레이어 스탯 업데이트
    public void F_UpdatePlayerState(int MaxHP = 0 ,int Attack = 0, int Defence = 0, int FlappyScore = 0)
    {
        if(_playerHP != MaxHP)
            _playerHP = MaxHP;

        this._playerAttack += Attack;
        this._playerSpeed += Defence;
        F_UpdateScore(FlappyScore);
    }

    public bool F_CheckHp(int hp) 
    {
        this._playerHP += hp;

        if (_playerHP <= 0 )
            return false;
        return true;
    }


    private void F_UpdateScore(int newScore) 
    {
        this._flappyGameScore = Mathf.Max(_flappyGameScore , newScore);
    }

    
}
#endregion
