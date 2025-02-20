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

        // 0�� lenght ���̿� �ε���
        if (_nowIndex < 0)
            _nowIndex = 0;
        if(_nowIndex > _hatList.Length - 1)
            _nowIndex = _hatList.Length - 1;

        // ������ �����ִ� hat ����
        _hatList[_preIndex].SetActive(false);
        // ���� hat �ѱ� 
        _hatList[_nowIndex].SetActive(true);

    }

}
