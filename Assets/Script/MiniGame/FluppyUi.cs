using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FluppyUi : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeCount;

    public void F_UpeateTimeCount(int time)
    {
        // 만약 꺼져있으면 켜기 
        if (!_timeCount.gameObject.activeSelf)
            _timeCount.gameObject.SetActive(true);

        if (time < 0)
            _timeCount.gameObject.SetActive(false);

        _timeCount.text = time.ToString();


    }
}
