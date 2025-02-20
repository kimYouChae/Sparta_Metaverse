using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustom : MonoBehaviour
{
    [Header("===Ui===")]
    [SerializeField] private GameObject[] _hatList;
    [SerializeField] private int _nowIndex = 0;
    [SerializeField] private int _preIndex = 0;
    [SerializeField] private Button _rightArrow;
    [SerializeField] private Button _leftArrow;

    public Sprite F_NowHat() => _hatList[_nowIndex].GetComponent<Image>().sprite;

    private void Awake()
    {
        _rightArrow.onClick.AddListener(F_ClickRight);
        _leftArrow.onClick.AddListener(F_ClickLeft);
    }

    private void F_ClickRight() 
    {
        _preIndex = _nowIndex;

        _nowIndex++;
        F_OnOffHat();
    }

    private void F_ClickLeft() 
    {
        _preIndex = _nowIndex;

        _nowIndex--;
        F_OnOffHat();
    }

    private void F_OnOffHat() 
    {

        // 0과 lenght 사이에 인덱스
        if (_nowIndex < 0)
            _nowIndex = 0;
        if(_nowIndex > _hatList.Length - 1)
            _nowIndex = _hatList.Length - 1;

        // 이전에 켜져있던 hat 끄기
        _hatList[_preIndex].SetActive(false);
        // 현재 hat 켜기 
        _hatList[_nowIndex].SetActive(true);

    }

}
