using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Transform _fluppyPlayerTrs;

    [Header("===State===")]
    [SerializeField] float _blockCoolTime = 3f;
    [SerializeField] float _endOfMap = -12f;
    [SerializeField] int _score = 0;

    // ������Ƽ
    public Transform FluppyPlayerTrs => _fluppyPlayerTrs;

    public void F_StartFlappyBird() 
    {
        _blockCoolTime = 3f;
        _endOfMap = -12f;
        _score = 0;

        StartCoroutine(IE_BlockGeneMove());
        StartCoroutine(IE_BlockMove());
    }

    public void F_StopFlappyBird() 
    {
        StopAllCoroutines();
    }

    IEnumerator IE_BlockMove() 
    {
        Transform _nowBlock;

        while (true) 
        {
            if (_generateBlock.Count == 0)
                continue;

            // �̵� 
            for(int i = 0; i < _generateBlock.Count; i++) 
            {
                _nowBlock = _generateBlock[i].transform;

                _nowBlock.position
                    = Vector3.Lerp(_nowBlock.transform.position,
                    new Vector3(_nowBlock.transform.position.x - 0.5f,
                    _nowBlock.transform.position.y , 0),
                    0.1f);
            }

            // ���� 
            for (int i = 0; i < _generateBlock.Count; i++)
            {
                _nowBlock = _generateBlock[i].transform;

                if (_nowBlock.localPosition.x <= _endOfMap) 
                {
                    // ����Ʈ���� ���� 
                    _generateBlock.Remove(_nowBlock);
                    // �� ���� 
                    Destroy(_nowBlock.gameObject);
                }
            }

            // �������Ӹ��� ����
            yield return new WaitForSeconds(0.02f); 
        }
    }

    IEnumerator IE_BlockGeneMove() 
    {
        // ##TODO : �� ��� �����ҰŸ� pooling��������

        int type;

        while (true) 
        {
            type = Random.Range(0, Enum.GetValues(typeof(BlockType)).Length);

            switch (type)
            {
                // Long�� ��, short�� �Ʒ� 
                case (int)BlockType.Long:
                    F_InstanBlock(_upGenerationTrs, _longBlock[Random.Range(0, _longBlock.Count)]);
                    F_InstanBlock(_downGenerationTrs, _shortBlock[Random.Range(0, _shortBlock.Count)], -1);
                    break;

                // short�� ��, long�� �Ʒ�
                case (int)BlockType.Short:
                    F_InstanBlock(_upGenerationTrs, _shortBlock[Random.Range(0, _shortBlock.Count)]);
                    F_InstanBlock(_downGenerationTrs, _longBlock[Random.Range(0, _longBlock.Count)], -1);
                    break;
            }

            // ���� ����
            _score += 10;

            yield return new WaitForSeconds(_blockCoolTime);
        }
    }

    private void F_InstanBlock( Transform trs, GameObject obj , int _rotationFlag = 1 ) 
    {
        // ����
        GameObject _blockInst =Instantiate(obj , trs.position , Quaternion.identity);
        // �θ����� 
        _blockInst.transform.parent = _blockParent;

        // ȸ�� (flag�� 1�̸� ȸ�� x, �׿ܴ� 180����)
        _blockInst.transform.eulerAngles = new Vector3(0,0, _rotationFlag == 1 ? 0 : 180f);

        // ������ �� ����Ʈ�� �ֱ� 
        _generateBlock.Add( _blockInst.transform );
    }
}
