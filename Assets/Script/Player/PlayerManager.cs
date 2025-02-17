using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerStateType 
{
    Village,            // 마을
    MinigameOne,        // 미니게임1
    MinigameTwo         // 미니게임2

}

public class PlayerManager : MonoBehaviour
{
    // ##TODO : 싱글톤은 상위에 올리기
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
