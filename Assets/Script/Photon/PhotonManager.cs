using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // �̱��� 
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

        // ���ӽ��� �� ���� ���� 
        // 1. ������ Ŭ���̾�Ʈ �� ����ȭ
        // 2. ���� 
        F_MasterClient();
    }

    private void F_MasterClient() 
    {
        // ������ Ŭ���̾��Ʈ�� LoadLevel �� , �� �ٸ� ������ �̵� �� 
        // �ٸ� ����鵵 ������Ŭ���̾�Ʈ�� �ִ� ������ �ڵ� �̵�
        PhotonNetwork.AutomaticallySyncScene = true;

        // ���ӹ��� �Ҵ� 
        PhotonNetwork.GameVersion = roomVersion;
        Debug.Log("���� ���� �ʴ� ������ ��� �� : " + PhotonNetwork.SendRate);

        // ** ���� ����ڵ��� ���� ������ ����
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();           // ** ���� ���� ���� ������ 

    }

    // ���� �����Ͱ� �����ϸ� ��� 
    public override void OnConnectedToMaster() 
    {
        // MonoBehaviourPunCallbacks �� OnConnected~�Լ��� �������̵�
        Debug.Log("���濡 ���� ���� ���� : " + PhotonNetwork.IsConnected);
    }

    public override void OnJoinedLobby()
    {
        // Photon���� Lobby�� �ǹ��ϴ°��� ?
        // ��Ƽ�÷��̾�� �÷��̾ "����"�� �� �ִ� ���� 
        // OnJoinedLobby �޼��� : ������ ������ �κ� ���� ���� �� ȣ��
    }
}
