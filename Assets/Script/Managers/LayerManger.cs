using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManger : Singleton<LayerManger>
{
    [Header("===Layer===")]
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _blockLayer;

    [Header("===Layer Num===")]
    [SerializeField] private int _playerLayerNum;
    [SerializeField] private int _blockLayerNum;

    public LayerMask PlayerLayer { get => _playerLayer; }
    public LayerMask BlockLayer { get => _blockLayer; }
    public int PlayerLayerNum { get => PlayerLayerNum1; }
    public int PlayerLayerNum1 { get => _playerLayerNum;}

    protected override void Singleton_Awake()
    {
        _playerLayer = LayerMask.GetMask("Player");
        _blockLayer = LayerMask.GetMask("Block");

        _playerLayerNum = LayerMask.NameToLayer("Player");
        _blockLayerNum = LayerMask.NameToLayer("Block");
    }
}
