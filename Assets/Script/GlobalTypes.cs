using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTypes
{

}

#region ENUM
public enum PlayerStateType
{
    Village,            // ����
    MinigameOne,        // �̴ϰ���1
    MinigameTwo         // �̴ϰ���2

}

enum BlockType
{
    Long, Short
}

#endregion

#region PlayerŬ����

[System.Serializable]
public class Player
{
    // �⺻����
    private string _playerName;
    private float _playerHP;
    private float _playerAttack;
    private float _playerSpeed;

    // ���� 
    private int _flappyGameScore;
    private int _otherGameScore;

    // ������Ƽ
    public string PlayerName { get => _playerName; set => _playerName = value; }
    public float PlayerHP { get => _playerHP; set => _playerHP = value; }
    public float PlayerAttack { get => _playerAttack; set => _playerAttack = value; }
    public float PlayerSpeed { get => _playerSpeed; set => _playerSpeed = value; }
    public int FlappyGameScore { get => _flappyGameScore; set => _flappyGameScore = value; }
    public int OtherGameScore { get => _otherGameScore; set => _otherGameScore = value; }

    // ������ 
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
