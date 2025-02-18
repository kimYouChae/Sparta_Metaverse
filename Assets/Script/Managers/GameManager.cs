using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 1. �÷��̾� ���� ����
    /// 2. �÷��̾� ���� ���̺� ����
    /// </summary>

    [Header("===Player Score Save=== ")]
    [SerializeField]
    private List<ScoreSaveClass> _playerScoreList;

    public List<ScoreSaveClass> playerScoreList => _playerScoreList;

    protected override void Singleton_Awake()
    {
        _playerScoreList = new List<ScoreSaveClass>();

        // �׽�Ʈ��
        /*
        _playerScoreList.Add(new ScoreSaveClass("1", 100));
        _playerScoreList.Add(new ScoreSaveClass("2", 30));
        _playerScoreList.Add(new ScoreSaveClass("3", 180));
        _playerScoreList.Add(new ScoreSaveClass("4", 10));

        F_SortByScore();
        
        for (int i = 0; i < _playerScoreList.Count; i++) 
        {
            UiManager.Instnace.F_AddToScoreList(_playerScoreList[i]);
        }
        */
    }

    public void F_SetPlayerScore(string name, int score)
    {
        if (_playerScoreList == null)
            _playerScoreList = new List<ScoreSaveClass>();

        int index = 0;

        // ���� ���� ���ص� ���°�� ū�� �˱⸸ �ϸ�ɵ� ?
        for (int i = 0; i < _playerScoreList.Count - 1; i++) 
        {
            if (score >= _playerScoreList[i + 1].Score && score <= playerScoreList[i].Score)
                index = i;
        }

        // ����Ʈ�� �߰� 
        ScoreSaveClass temp = new ScoreSaveClass(name, score);
        _playerScoreList.Insert( index , temp);

        // Ui ������Ʈ
        UiManager.Instnace.F_AddToScoreList(index, temp);

    }

    // ���� ���������� ����
    public void F_SortByScore()
    {
        _playerScoreList.Sort(CompareIntMethod);

        // Ȯ�ο� ��� 
        /* 
        for (int i = 0; i < _playerScoreList.Count; i++) 
        {
            Debug.Log($"{i}�� : {_playerScoreList[i].Name} + {_playerScoreList[i].Score} �� ");
        }
        */
    }
    public int CompareIntMethod(ScoreSaveClass c1, ScoreSaveClass c2)
    {
        return c2.Score.CompareTo(c1.Score);
    }
}
