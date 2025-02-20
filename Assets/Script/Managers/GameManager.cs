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

    [Header("===save manager===")]
    [SerializeField] SaveManager _saveManager;

    [Header("===Video manager use===")]
    public GameObject _videoMangerChatObject;
    public Transform _videoMangerLayout;

    // ���ӽ��� ��ư Ŭ�� �� ������ ��������Ʈ
    public delegate void DEL_PlayerCreateEventHandler();            // �÷��̾ ���� ��/ ������ �÷��̾���� �Ҵ��ϴ� ��������Ʈ
    public event DEL_PlayerCreateEventHandler Del_playerCreated;

    public List<ScoreSaveClass> playerScoreList => _playerScoreList;

    protected override void Singleton_Awake()
    {
        _playerScoreList = new List<ScoreSaveClass>();

        // ������ Load + ui ������Ʈ
        F_LoadScoreAndUpdateUi();

        // �׽�Ʈ��
        /*
        _playerScoreList.Add(new ScoreSaveClass("1", 100));
        _playerScoreList.Add(new ScoreSaveClass("2", 30));
        _playerScoreList.Add(new ScoreSaveClass("3", 180));
        _playerScoreList.Add(new ScoreSaveClass("4", 10));

        F_SortByScore();
        */
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            _saveManager.F_Save();
    }

    // UiManager���� Start��ư Ŭ�� �� ���� 
    public void OnPlayerCreate()
    {
        Del_playerCreated?.Invoke();
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

    private void F_LoadScoreAndUpdateUi() 
    {
        // ������ �ҷ�����
        ScoreSaveWrapper wrapper = _saveManager.F_Load();
        if (wrapper == null)
            return;

        // ����Ʈ�� �����ؼ� �ֱ�
        _playerScoreList = wrapper.ScroeSaveList;

        // ����
        F_SortByScore();

        // Ui�� �ֱ� 
        for (int i = 0; i < _playerScoreList.Count; i++)
        {
            UiManager.Instnace.F_AddToScoreList(i, _playerScoreList[i]);
        }
    }
}
