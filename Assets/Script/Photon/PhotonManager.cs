using Photon.Pun;
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
    string roomVersion = "1.0.0";
    string roomName = "BlackVillage";

    private void Awake()
    {
        if(instance == null)
            instance = this;    
        else if( instance != this)
            Destroy(instance);

        DontDestroyOnLoad(instance);

        // 게임시작 시 포톤 세팅 
        // 1. 마스터 클라이언트 씬 동기화
        // 2. 연결 
        F_MasterClient();
    }

    private void F_MasterClient() 
    {
        // 마스터 클라이어언트가 LoadLevel 시 , 즉 다른 씬으로 이동 시 
        // 다른 멤버들도 마스터클라이언트가 있는 씬으로 자동 이동
        PhotonNetwork.AutomaticallySyncScene = true;

        // 게임버전 할당 
        PhotonNetwork.GameVersion = roomVersion;
        Debug.Log("포톤 서버 초당 데이터 통신 수 : " + PhotonNetwork.SendRate);

        // ** 포톤 사용자들을 포톤 서버에 연결
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();           // ** 포톤 서버 세팅 정보로 

    }

    // 서버 마스터가 접속하면 출력 
    public override void OnConnectedToMaster() 
    {
        // MonoBehaviourPunCallbacks 의 OnConnected~함수를 오버라이딩
        Debug.Log("포톤에 접속 성공 유무 : " + PhotonNetwork.IsConnected);
    }

    public override void OnJoinedLobby()
    {
        // Photon에서 Lobby가 의미하는것은 ?
        // 멀티플레이어에서 플레이어가 "입장"할 수 있는 공간 
        // OnJoinedLobby 메서드 : 마스터 서버의 로비에 입장 했을 때 호출
    }
}
