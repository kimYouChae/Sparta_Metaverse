using System.Collections;
using System.Collections.Generic;
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

    protected override void Singleton_Awake()
    {
        // ���ù�ư : end�˾� on off
        _settingButton.onClick.AddListener(() => _endGamePopUp.SetActive( !_endGamePopUp.activeSelf));
        // �κ��丮 ��ư : �κ��丮 �˾� on off
        _inventoryButton.onClick.AddListener(() => _inventoryPopUp.SetActive( !_inventoryPopUp.activeSelf ));

        // ���ӽ��۹�ư : ���ӽ��� ���� 
        _enterGameButton.onClick.AddListener( PlayerManager.Instnace.F_EnterGame );
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

}
