using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerStateType 
{
    Village,            // ����
    MinigameOne,        // �̴ϰ���1
    MinigameTwo         // �̴ϰ���2

}

public class PlayerManager : MonoBehaviour
{
    // ##TODO : �̱����� ������ �ø���
    static private PlayerManager instance;
    public static PlayerManager Instnace { get => instance; }

    [Header("===Player===")]
    [SerializeField] private Transform _playerTrs;

    public Transform playerTrs => _playerTrs;

    private void Awake()
    {
        instance = this;
    }

}
