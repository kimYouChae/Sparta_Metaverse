using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

using Random = UnityEngine.Random;

public class BlockGenerate : MonoBehaviour
{
    [Header("===Block===")]
    [SerializeField] private List<GameObject> _longBlock;
    [SerializeField] private List<GameObject> _shortBlock;
    [SerializeField] private List<Transform> _generateBlock;
    [SerializeField] private Transform _blockParent;
    
    [Header("===Position===")]
    [SerializeField] private Transform _upGenerationTrs;
    [SerializeField] private Transform _downGenerationTrs;


    [Header("===State===")]
    [SerializeField] float _blockCoolTime = 3f;
    [SerializeField] float _endOfMap = -12f;
    [SerializeField] int _score = 0;

    [Header("===Ui===")]
    [SerializeField] private FluppyUi _fluppuUi;

    // 프로퍼티
    public int FluppyScore => _score;

    public void F_StartFlappyBird() 
    {
        // 값 초기화 
        _blockCoolTime = 3f;
        _endOfMap = -12f;
        _score = 0;

        // 플레이어 hp도 초기화 
        float max = PlayerManager.Instnace.nowPlayer.PlayerMaxHp;
        PlayerManager.Instnace.nowPlayer.F_UpdatePlayerState(MaxHP : (int)max);

        // 점수 텍스트 초기화 
        MiniGameManager.Instnace.fluidUi.F_UpdateScoreText(_score);
        // 하트 초기화
        MiniGameManager.Instnace.fluidUi.F_OnAllHeart();

        StartCoroutine(IE_WaitBeforeGame());
    }

    IEnumerator IE_WaitBeforeGame() 
    {
        for (int i = 3; i >= 1; i--) 
        {
            // UI에 카운터
            _fluppuUi.F_UpeateTimeCount(i);

            // 1초 대기 
            yield return new WaitForSeconds(1f);
        }

        _fluppuUi.F_UpeateTimeCount(-1);

        // 카운터 후 플레이어 중력 조정
        PlayerManager.Instnace.playerMovement.F_OnOffGravity();

        // 블럭 생성, 움직임, 삭제 동작
        StartCoroutine(IE_BlockGeneMove());
        StartCoroutine(IE_BlockMove());
    }

    public void F_StopFlappyBirdCoru() 
    {
        StopAllCoroutines();

        // 만들어놓은 block 들 없애기 
        for (int i = _generateBlock.Count - 1; i >= 0; i--) 
        {
            Transform _temp = _generateBlock[i];
            _generateBlock.Remove(_temp);
            Destroy(_temp.gameObject);
        }
    }

    IEnumerator IE_BlockMove() 
    {
        Transform _nowBlock;

        while (true) 
        {
            if (_generateBlock.Count == 0)
                continue;

            // 이동 
            for(int i = 0; i < _generateBlock.Count; i++) 
            {
                _nowBlock = _generateBlock[i].transform;

                _nowBlock.position
                    = Vector3.Lerp(_nowBlock.transform.position,
                    new Vector3(_nowBlock.transform.position.x - 0.5f,
                    _nowBlock.transform.position.y , 0),
                    0.1f);
            }

            // 삭제 
            for (int i = 0; i < _generateBlock.Count; i++)
            {
                _nowBlock = _generateBlock[i].transform;

                if (_nowBlock.localPosition.x <= _endOfMap) 
                {
                    // 리스트에서 삭제 
                    _generateBlock.Remove(_nowBlock);
                    // 블럭 삭제 
                    Destroy(_nowBlock.gameObject, 1f);
                }
            }

            // 매프레임마다 돌기
            yield return new WaitForSeconds(0.02f); 
        }
    }

    IEnumerator IE_BlockGeneMove() 
    {
        // ##TODO : 블럭 계속 생성할거면 pooling만들어야함

        int type;

        while (true) 
        {
            type = Random.Range(0, Enum.GetValues(typeof(BlockType)).Length);

            switch (type)
            {
                // Long을 위, short가 아래 
                case (int)BlockType.Long:
                    F_InstanBlock(_upGenerationTrs, _longBlock[Random.Range(0, _longBlock.Count)]);
                    F_InstanBlock(_downGenerationTrs, _shortBlock[Random.Range(0, _shortBlock.Count)], -1);
                    break;

                // short가 위, long이 아래
                case (int)BlockType.Short:
                    F_InstanBlock(_upGenerationTrs, _shortBlock[Random.Range(0, _shortBlock.Count)]);
                    F_InstanBlock(_downGenerationTrs, _longBlock[Random.Range(0, _longBlock.Count)], -1);
                    break;
            }

            // 점수 증가
            _score += 10;
            // 점수 ui 업데이트 
            MiniGameManager.Instnace.fluidUi.F_UpdateScoreText(_score);

            yield return new WaitForSeconds(_blockCoolTime);
        }
    }

    private void F_InstanBlock( Transform trs, GameObject obj , int _rotationFlag = 1 ) 
    {
        // 생성
        GameObject _blockInst =Instantiate(obj , trs.position , Quaternion.identity);
        // 부모지정 
        _blockInst.transform.parent = _blockParent;

        // 회전 (flag가 1이면 회전 x, 그외는 180으로)
        _blockInst.transform.eulerAngles = new Vector3(0,0, _rotationFlag == 1 ? 0 : 180f);

        // 생성한 블럭 리스트에 넣기 
        _generateBlock.Add( _blockInst.transform );
    }
}
