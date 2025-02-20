using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class UiManager : Singleton<UiManager>
{
    [Header("===Panel===")]
    [SerializeField] private GameObject _beforePlayerInstance;    //플레이어 생성 전 ui
    [SerializeField] private GameObject _villagePanel;      // 마을 ui
    [SerializeField] private GameObject _fluppyPanel;       // 미니게임1 (플러피) ui

    [Header("===PopUp===")]
    [SerializeField] private GameObject _endGamePopUp;
    [SerializeField] private GameObject _inventoryPopUp;
    [SerializeField] private GameObject _miniGamePopup;
    [SerializeField] private GameObject _scorePopUp;

    [Header("===Button===")]
    [SerializeField] private Button _startGmaeButton;       // 게임시작 버튼
    [SerializeField] private Button _settingButton;         // 세팅 버튼 
    [SerializeField] private Button _inventoryButton;       // 인벤토리 버튼
    [SerializeField] private Button _enterGameButton;       // 미니 게임시작 버튼

    [Header("===Text===")]
    [SerializeField] private TextMeshProUGUI _beforePlayerText; 

    [Header("===Prefab===")]
    [SerializeField] private GameObject _scoreContextPrefab;    // 이름 + 점수 프리팹

    [Header("===Score Scroll View===")]
    [SerializeField] Transform _content;               // 스크롤뷰의 Context 

    [Header("===Text Input Field===")]
    [SerializeField] TMP_InputField _nameInputField;

    public Transform _canvas;
    public GameObject _conferenceCameraPanel;

    protected override void Singleton_Awake()
    {
       
    }

    private void Start()
    {
        GameManager.Instnace.Del_playerCreated += () => _beforePlayerInstance.SetActive(false);

        // 게임 입장 버튼 : Photonmanager의 델리게이트 실행
        _startGmaeButton.onClick.AddListener(() => GameManager.Instnace.OnPlayerCreate());

        // 세팅버튼 : end팝업 on off
        _settingButton.onClick.AddListener(() => _endGamePopUp.SetActive(!_endGamePopUp.activeSelf));
        // 인벤토리 버튼 : 인벤토리 팝업 on off
        _inventoryButton.onClick.AddListener(() => _inventoryPopUp.SetActive(!_inventoryPopUp.activeSelf));

        // 미니 게임시작버튼 : 게임시작 로직 
        _enterGameButton.onClick.AddListener(PlayerManager.Instnace.F_EnterMiniGame);
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

        // 상위빈오브젝트
        //     ㄴ 프로필
        //     ㄴ 이름
        //     ㄴ 점수

        // 부모오브젝트
        _obj.SetParent(_content , true);
        _obj.SetSiblingIndex(idx);      // content의 index번째 자식으로
        _obj.GetChild(1).GetComponent<TextMeshProUGUI>().text = scoreclass.Name;
        _obj.GetChild(2).GetComponent<TextMeshProUGUI>().text = scoreclass.Score.ToString();
    }

    public void F_BeforeGameStart(int cnt) 
    {
        _beforePlayerText.text = cnt.ToString() + " 초";

        // 0이면 -> 입장가능
        if (cnt <= 0)
        { 
            // 입장 버튼 On
            F_EnterButton();
        }

    }

    public string F_InputName() 
    {
        return _nameInputField.text;
    }

    private void F_EnterButton() 
    {
        // 캐릭터 생성 -> 마을 입장 버튼 
        _startGmaeButton.gameObject.SetActive(true);
    }
}
