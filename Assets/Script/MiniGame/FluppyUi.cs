using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FluppyUi : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeCount;
    [SerializeField]
    private GameObject[] _heartIconList;

    public void F_UpeateTimeCount(int time)
    {
        // ���� ���������� �ѱ� 
        if (!_timeCount.gameObject.activeSelf)
            _timeCount.gameObject.SetActive(true);

        if (time < 0)
            _timeCount.gameObject.SetActive(false);

        _timeCount.text = time.ToString();
    }

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
}
