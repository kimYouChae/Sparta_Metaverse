using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FluppyUi : MonoBehaviour
{
    [Header("===Text===")]
    [SerializeField]
    private TextMeshProUGUI _timeCount;
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [Header("===Ui===")]
    [SerializeField]
    private GameObject[] _heartIconList;
    [SerializeField]
    private GameObject _diePopUp;
    [SerializeField]
    private Button _dieCheckButton;

    private void Start()
    {
        _dieCheckButton.onClick.AddListener( PlayerManager.Instnace.F_ClickDiePopUp );
    }

    public void F_UpeateTimeCount(int time)
    {
        // 만약 꺼져있으면 켜기 
        if (!_timeCount.gameObject.activeSelf)
            _timeCount.gameObject.SetActive(true);

        // -1이 들어오면 끄기 
        if (time < 0)
            _timeCount.gameObject.SetActive(false);

        _timeCount.text = time.ToString();
    }

    // 하트아이콘 한개씩 off
    public void F_UpdateHeartIcon(int reduceCount) 
    {
        int _nowCount = 0;

        for (int i = _heartIconList.Length - 1; i >= 0 ; i--) 
        {
            if (_nowCount == reduceCount)
                break;

            // 꺼져있으면 패스 
            if (!_heartIconList[i].activeSelf)
                continue;

            // 끄기 
            _heartIconList[i].SetActive(false);     
            
            _nowCount++;
        }
    }

    // 하트아이콘 다 on
    public void F_OnAllHeart() 
    {
        for (int i = _heartIconList.Length - 1; i >= 0; i--)
        {
            // 꺼져있으면 켜기 
            if (!_heartIconList[i].activeSelf)
                _heartIconList[i].SetActive(true);
        }
    }


    public void F_OnOffDiePopUp( bool flag ) 
    {
        _diePopUp.SetActive(flag);
    }

    public void F_UpdateScoreText(int score) 
    {
        _scoreText.text = score.ToString();
    }
}
