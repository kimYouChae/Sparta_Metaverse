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
        // ���� ���������� �ѱ� 
        if (!_timeCount.gameObject.activeSelf)
            _timeCount.gameObject.SetActive(true);

        // -1�� ������ ���� 
        if (time < 0)
            _timeCount.gameObject.SetActive(false);

        _timeCount.text = time.ToString();
    }

    // ��Ʈ������ �Ѱ��� off
    public void F_UpdateHeartIcon(int reduceCount) 
    {
        int _nowCount = 0;

        for (int i = _heartIconList.Length - 1; i >= 0 ; i--) 
        {
            if (_nowCount == reduceCount)
                break;

            // ���������� �н� 
            if (!_heartIconList[i].activeSelf)
                continue;

            // ���� 
            _heartIconList[i].SetActive(false);     
            
            _nowCount++;
        }
    }

    // ��Ʈ������ �� on
    public void F_OnAllHeart() 
    {
        for (int i = _heartIconList.Length - 1; i >= 0; i--)
        {
            // ���������� �ѱ� 
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
