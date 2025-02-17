using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // ##TODO : ½Ì±ÛÅæÀº »óÀ§¿¡ ¿Ã¸®±â
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
