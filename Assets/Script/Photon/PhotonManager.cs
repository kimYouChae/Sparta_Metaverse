using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // 싱글톤 
    private static PhotonManager instance = null;
    public static PhotonManager Instnace { get => instance; }

    [Header("===Info===")]
    [SerializeField] string _roomVersion = "1.0.0";
    [SerializeField] string _roomName = "BlackVillage";

    [Header("===동기화 Player===")]
    [SerializeField] private GameObject _player;                    // 생성할 플레이어 오브젝트
    private int _waitforPlayerInstance = 5;
    
    public GameObject photonPlayer => _player; 

    private void Awake()
    {
        if(instance == null)
            instance = this;    
        else if( instance != this)
            Destroy(instance);

        DontDestroyOnLoad(instance);

        // 게임시작 시 포톤 세팅 
        // 1. 포톤서버에 연결, 로비 생성 후 방 생성
        F_MasterClient();

        // 2. 플레이어 생성 
        StartCoroutine(CreatePlayer());
    }

    IEnumerator CreatePlayer() 
    {
        // 실행 이후 즉시 생성보다 여유시간을 주고 생성 (혹시 모를 오류 방지)
        for (int i = _waitforPlayerInstance; i >= 0; i--) 
        {
            // Ui 업데이트
            UiManager.Instnace.F_BeforeGameStart(i);

            // 1초 기다리기
            yield return new WaitForSeconds(1);
        }

        // 입력받은 닉네임 가져오기 
        PhotonNetwork.NickName = UiManager.Instnace.F_InputName();

        // 플레이어 생성 
        // PhotonNetwork의 Instantiate 사용 
        _player = PhotonNetwork.Instantiate("Player", new Vector3(0,0,0), Quaternion.identity);

        if (_player == null)
        {
            Debug.Log("생성한 플레이어가 null");
            yield break;
        }

        // 플레이어를 생성하기 전 세팅
        // 1. Resources 폴더에 "Player" 라는 이름의 프리팹이 있어야함
        // 2. 포톤 클라우드 서버에 동기화
        // : Photon View, Photon Transform , Photon Animator View 컴포넌트가 부착되어 있어야함

        // 플레어어 생성 완료 델리게이트 실행 -> [2.20수정] UiManager에 버튼 클릭시 실행되게 
        // OnPlayerCreate();
    }

    #region 포톤 서버 연결, 포톤 로비 생성 후 방 (room) 생성

    private void F_MasterClient() 
    {
        // 마스터 클라이어언트가 LoadLevel 시 , 즉 다른 씬으로 이동 시 
        // 다른 멤버들도 마스터클라이언트가 있는 씬으로 자동 이동
        PhotonNetwork.AutomaticallySyncScene = true;

        // 게임버전 할당 
        PhotonNetwork.GameVersion = _roomVersion;
        Debug.Log("포톤 서버 초당 데이터 통신 수 : " + PhotonNetwork.SendRate);

        // ** 포톤 사용자들을 포톤 서버에 연결
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();           // ** 포톤 서버 세팅 정보로 

    }

    public override void OnConnectedToMaster() 
    {
        // MonoBehaviourPunCallbacks 의 OnConnected~함수를 오버라이딩
        // 서버 마스터가 접속하면 메서드 실행 
        Debug.Log("포톤에 접속 성공 유무 : " + PhotonNetwork.IsConnected);

        // 로비 접속 추가 
        OnJoinedLobby();
    }

    public override void OnJoinedLobby()
    {
        // Photon에서 Lobby가 의미하는것은 ?
        // 멀티플레이어에서 플레이어가 입장하기 전 가상의 대기공간
        // 서버 접속(Master Server) -> 로비 접속(Lobby) -> 룸 생성/접속(Room) -> 게임 씬 로드
        // 물리적인 씬 과는 상관이 없음 

        // 로비에 접속하면 실행

        // Debug.Log("로비접속 "  + PhotonNetwork.InLobby);
        JointRoom();
    }

    private void JointRoom() 
    {
        // 마스터만 방 생성 
        // 방 (room) 생성하는 함수 정의
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        // 생성 후 바로 입장
        PhotonNetwork.JoinOrCreateRoom(_roomName , roomOptions , TypedLobby.Default);
        // -> 방 이름 기준으로 작동함
        // 즉 마스터클라이언트가 방을 만들면, 다음 접속자는 이름에 해당하는 방이 이미 있으니 거기로 접속 
    }

    public override void OnCreatedRoom()
    {
        // 방 생성시 호출 (오버라이딩)
        Debug.Log("방 생성 완료 : " + PhotonNetwork.CurrentRoom.Name );
    }

    public override void OnJoinedRoom()
    {
        // 방 입장 시 호출 (오버라이딩)
        Debug.Log($"{PhotonNetwork.CurrentRoom.Name} 에 입장 / 방에 있는지 여부 : {PhotonNetwork.InRoom}" +
            $" 인원 수 : {PhotonNetwork.CurrentRoom.PlayerCount}" );
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 방 접속 실패 시 호출 (오버라이딩)
        Debug.Log($"JoinRoom Failed {returnCode} {message} / 방 없음 -> 생성시도중....");
        JointRoom();
    }
    #endregion
}
