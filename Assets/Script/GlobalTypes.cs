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
    private string _playerName;
    private float _playerHP;
    private float _playerAttack;
    private float _playerSpeed;

    // 점수 
    private int _flappyGameScore;
    private int _otherGameScore;

    // 프로퍼티
    public string PlayerName { get => _playerName; set => _playerName = value; }
    public float PlayerHP { get => _playerHP; set => _playerHP = value; }
    public float PlayerAttack { get => _playerAttack; set => _playerAttack = value; }
    public float PlayerSpeed { get => _playerSpeed; set => _playerSpeed = value; }
    public int FlappyGameScore { get => _flappyGameScore; set => _flappyGameScore = value; }
    public int OtherGameScore { get => _otherGameScore; set => _otherGameScore = value; }

    // 생성자 
    public Player(string name, float hp, float att, float df)
    {
        this._playerName = name;
        this._playerHP = hp;
        this._playerAttack = att;
        this._playerSpeed = df;
        this._flappyGameScore = 0;
        this._otherGameScore = 0;
    }

    public bool F_UpdateHp(int mount) 
    {
        _playerHP += mount;

        if (_playerHP <= 0)
            return false;
        else
            return true;
    }

    
}
#endregion
