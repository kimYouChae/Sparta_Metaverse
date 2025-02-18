using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 1. 플레이어 점수 관리
    /// 2. 플레이어 점수 세이브 관리
    /// </summary>

    [Header("===Player Score Save=== ")]
    [SerializeField]
    private List<ScoreSaveClass> _playerScoreList;

    [Header("===save manager===")]
    [SerializeField] SaveManager _saveManager;

    public List<ScoreSaveClass> playerScoreList => _playerScoreList;

    protected override void Singleton_Awake()
    {
        _playerScoreList = new List<ScoreSaveClass>();

        // 데이터 Load + ui 업데이트
        F_LoadScoreAndUpdateUi();

        // 테스트용
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

    public void F_SetPlayerScore(string name, int score)
    {
        if (_playerScoreList == null)
            _playerScoreList = new List<ScoreSaveClass>();

        int index = 0;

        // 굳이 정렬 안해도 몇번째로 큰지 알기만 하면될듯 ?
        for (int i = 0; i < _playerScoreList.Count - 1; i++) 
        {
            if (score >= _playerScoreList[i + 1].Score && score <= playerScoreList[i].Score)
                index = i;
        }

        // 리스트에 추가 
        ScoreSaveClass temp = new ScoreSaveClass(name, score);
        _playerScoreList.Insert( index , temp);

        // Ui 업데이트
        UiManager.Instnace.F_AddToScoreList(index, temp);

    }

    // 높은 점수순으로 정렬
    public void F_SortByScore()
    {
        _playerScoreList.Sort(CompareIntMethod);

        // 확인용 출력 
        /* 
        for (int i = 0; i < _playerScoreList.Count; i++) 
        {
            Debug.Log($"{i}등 : {_playerScoreList[i].Name} + {_playerScoreList[i].Score} 점 ");
        }
        */
    }
    public int CompareIntMethod(ScoreSaveClass c1, ScoreSaveClass c2)
    {
        return c2.Score.CompareTo(c1.Score);
    }

    private void F_LoadScoreAndUpdateUi() 
    {
        // 데이터 불러오기
        ScoreSaveWrapper wrapper = _saveManager.F_Load();
        if (wrapper == null)
            return;

        // 리스트에 복사해서 넣기
        _playerScoreList = wrapper.ScroeSaveList;

        // 정렬
        F_SortByScore();

        // Ui에 넣기 
        for (int i = 0; i < _playerScoreList.Count; i++)
        {
            UiManager.Instnace.F_AddToScoreList(i, _playerScoreList[i]);
        }
    }
}
