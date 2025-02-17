using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : Singleton<MiniGameManager>
{
    [Header("===Script===")]
    [SerializeField] private BlockGenerate _fluppyBirdGame;
    [SerializeField] private FluppyUi _fluppyUi;

    [Header("===Game Start Player Trs===")]
    [SerializeField] private Transform _fluppyPlayerTrs;

    // 프로퍼티
    public Transform FluppyPlayerTrs => _fluppyPlayerTrs;
    public BlockGenerate fluppyBirdGame => _fluppyBirdGame;
    public FluppyUi fluidUi => _fluppyUi;
    
    protected override void Singleton_Awake()
    {
        
    }

   
}
