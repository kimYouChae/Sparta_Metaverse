using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManger : Singleton<LayerManger>
{
    [Header("===Layer===")]
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _blockLayer;
    [SerializeField] private LayerMask _gameEnteranceLayer;
    [SerializeField] private LayerMask _gameScoreBoardLayer;
    
    [Header("===Layer Num===")]
    [SerializeField] private int _playerLayerNum;
    [SerializeField] private int _blockLayerNum;
    [SerializeField] private int _gameEnteranceLayerNum;
    [SerializeField] private int _gameScoreBoardLayerNum;

    public LayerMask PlayerLayer { get => _playerLayer; }
    public LayerMask BlockLayer { get => _blockLayer; }
    public LayerMask GameEnteranceLayer { get => _gameEnteranceLayer;  }
    public LayerMask GameScoreBoardLayer { get => _gameScoreBoardLayer;  }
    public int PlayerLayerNum { get => _playerLayerNum; }
    public int BlockLayerNum { get => _blockLayerNum;  }
    public int GameEnteranceLayerNum { get => _gameEnteranceLayerNum;  }
    public int GameScoreBoardLayerNum { get => _gameScoreBoardLayerNum; }

    protected override void Singleton_Awake()
    {
        _playerLayer = LayerMask.GetMask("Player");
        _blockLayer = LayerMask.GetMask("Block");
        _gameEnteranceLayer = LayerMask.GetMask("GameEnterance");
        _gameScoreBoardLayer = LayerMask.GetMask("GameBorad");

        _playerLayerNum = LayerMask.NameToLayer("Player");
        _blockLayerNum = LayerMask.NameToLayer("Block");
        _gameEnteranceLayerNum = LayerMask.NameToLayer("GameEnterance");
        _gameScoreBoardLayerNum = LayerMask.NameToLayer("GameBorad");
    }
}
