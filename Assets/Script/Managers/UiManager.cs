using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    [Header("===Panel===")]
    [SerializeField] private GameObject _villagePanel;      // ���� ui
    [SerializeField] private GameObject _fluppyPanel;       // �̴ϰ���1 (�÷���) ui

    [Header("===PopUp===")]
    [SerializeField] private GameObject _endGamePopUp;
    [SerializeField] private GameObject _inventoryPopUp;

    [Header("===Button===")]
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _inventoryButton;

    [Header("===Text===")]
    [SerializeField] private GameObject _timeText;

    protected override void Singleton_Awake()
    {
        // ���ù�ư : end�˾� on off
        _settingButton.onClick.AddListener(() => _endGamePopUp.SetActive( !_endGamePopUp.activeSelf));
        // �κ��丮 ��ư : �κ��丮 �˾� on off
        _inventoryButton.onClick.AddListener(() => _inventoryPopUp.SetActive( !_inventoryPopUp.activeSelf ));
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

    private void F_UpdateUi(GameObject ui , bool flag) 
    {
        ui.SetActive( flag );
    }

}
