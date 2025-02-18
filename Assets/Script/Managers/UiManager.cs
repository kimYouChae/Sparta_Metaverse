using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    [Header("===Panel===")]
    [SerializeField] private GameObject _villagePanel;      // 마을 ui
    [SerializeField] private GameObject _fluppyPanel;       // 미니게임1 (플러피) ui

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
        // 세팅버튼 : end팝업 on off
        _settingButton.onClick.AddListener(() => _endGamePopUp.SetActive( !_endGamePopUp.activeSelf));
        // 인벤토리 버튼 : 인벤토리 팝업 on off
        _inventoryButton.onClick.AddListener(() => _inventoryPopUp.SetActive( !_inventoryPopUp.activeSelf ));
    }

    // player타입에 따라 panel on off
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
