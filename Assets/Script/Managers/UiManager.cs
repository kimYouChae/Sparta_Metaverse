using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class UiManager : Singleton<UiManager>
{
    [Header("===Panel===")]
    [SerializeField] private GameObject _villagePanel;      // ���� ui
    [SerializeField] private GameObject _fluppyPanel;       // �̴ϰ���1 (�÷���) ui

    [Header("===PopUp===")]
    [SerializeField] private GameObject _endGamePopUp;
    [SerializeField] private GameObject _inventoryPopUp;
    [SerializeField] private GameObject _miniGamePopup;
    [SerializeField] private GameObject _scorePopUp;

    [Header("===Button===")]
    [SerializeField] private Button _settingButton;         // ���� ��ư 
    [SerializeField] private Button _inventoryButton;       // �κ��丮 ��ư
    [SerializeField] private Button _enterGameButton;       // ���ӽ��� ��ư
    
    [Header("===Text===")]
    [SerializeField] private GameObject _timeText;

    [Header("===Prefab===")]
    [SerializeField] private GameObject _scoreContextPrefab;    // �̸� + ���� ������

    [Header("===Score Scroll View===")]
    [SerializeField] Transform _content;               // ��ũ�Ѻ��� Context 

    protected override void Singleton_Awake()
    {
       
    }

    private void Start()
    {
        // ���ù�ư : end�˾� on off
        _settingButton.onClick.AddListener(() => _endGamePopUp.SetActive(!_endGamePopUp.activeSelf));
        // �κ��丮 ��ư : �κ��丮 �˾� on off
        _inventoryButton.onClick.AddListener(() => _inventoryPopUp.SetActive(!_inventoryPopUp.activeSelf));

        // ���ӽ��۹�ư : ���ӽ��� ���� 
        _enterGameButton.onClick.AddListener(PlayerManager.Instnace.F_EnterGame);
    }

    // playerŸ�Կ� ���� panel on off
    public void F_OnOffPanelByState(PlayerStateType _type) 
    {
        switch(_type) 
        {
            case PlayerStateType.Village:
                F_UpdateUi(_villagePanel , true);
                F_UpdateUi(_fluppyPanel , false);
                break;
            case PlayerStateType.MinigameOne:
                F_UpdateUi(_villagePanel ,false);
                F_UpdateUi(_fluppyPanel , true);
                break;
        }
    }

    public void F_OnOFfMiniGamePopUp(bool flag) 
    {
        F_UpdateUi(_miniGamePopup , flag);
    }

    public void F_OnOffScorePopUp(bool flag) 
    {
        F_UpdateUi(_scorePopUp, flag);
    }

    private void F_UpdateUi(GameObject ui , bool flag) 
    {
        ui.SetActive( flag );
    }

    public void F_AddToScoreList(int idx ,ScoreSaveClass scoreclass) 
    {
        //Debug.Log($"{idx} + {scoreclass.Name} / {scoreclass.Score}");
        Transform _obj = Instantiate(_scoreContextPrefab).transform;

        // �����������Ʈ
        //     �� ������
        //     �� �̸�
        //     �� ����

        // �θ������Ʈ
        _obj.SetParent(_content , true);
        _obj.SetSiblingIndex(idx);      // content�� index��° �ڽ�����
        _obj.GetChild(1).GetComponent<TextMeshProUGUI>().text = scoreclass.Name;
        _obj.GetChild(2).GetComponent<TextMeshProUGUI>().text = scoreclass.Score.ToString();
    }

}
