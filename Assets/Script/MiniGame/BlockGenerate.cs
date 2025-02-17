using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

enum BlockType 
{
    Long, Short
}

public class BlockGenerate : MonoBehaviour
{
    [Header("===Block===")]
    [SerializeField] private List<GameObject> _longBlock;
    [SerializeField] private List<GameObject> _shortBlock;
    [SerializeField] private List<GameObject> _generateBlock;
    [SerializeField] private Transform _blockParent;
    
    [Header("===GenePosition===")]
    [SerializeField] private Transform _upGenerationTrs;
    [SerializeField] private Transform _downGenerationTrs;

    [SerializeField] float _blockCoolTime = 3f;
    

    private void Start()
    {
        StartCoroutine(IE_BlockGeneMove());
    }

    IEnumerator IE_BlockGeneMove() 
    {
        // ##TODO : �� ��� �����ҰŸ� pooling��������

        int type = Random.Range(0, Enum.GetValues(typeof(BlockType)).Length);

        switch (type) 
        {
            // Long�� ��, short�� �Ʒ� 
            case (int)BlockType.Long:
                F_InstanBlock( _upGenerationTrs , _longBlock[ Random.Range(0, _longBlock.Count )]);
                F_InstanBlock( _downGenerationTrs , _shortBlock[ Random.Range(0, _shortBlock.Count )] , -1);
                break;

            // short�� ��, long�� �Ʒ�
            case (int)BlockType.Short:
                F_InstanBlock(_upGenerationTrs, _shortBlock[Random.Range(0, _shortBlock.Count)]);
                F_InstanBlock(_downGenerationTrs, _longBlock[Random.Range(0, _longBlock.Count)] , -1);
                break;
        }


        yield return new WaitForSeconds(_blockCoolTime);
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
        _generateBlock.Add( _blockInst );
    }
}
