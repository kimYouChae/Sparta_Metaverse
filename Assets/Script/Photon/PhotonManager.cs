using Photon.Pun;
using Photon.Realtime;
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
    [SerializeField] string roomVersion = "1.0.0";
    [SerializeField] string roomName = "BlackVillage";

    [Header("===����ȭ Player===")]
    [SerializeField] private GameObject _player;            // ������ �÷��̾� ������Ʈ
    public delegate void PlayerCreateEventHandler();        // �÷��̾ ���� ��/ ������ �÷��̾���� �Ҵ��ϴ� ��������Ʈ
    public event PlayerCreateEventHandler playerCreated;

    public GameObject photonPlayer => _player; 

    private void Awake()
    {
        if(instance == null)
            instance = this;    
        else if( instance != this)
            Destroy(instance);

        DontDestroyOnLoad(instance);

        // ���ӽ��� �� ���� ���� 
        // 1. ���漭���� ����, �κ� ���� �� �� ����
        F_MasterClient();

        // 2. �÷��̾� ���� 
        StartCoroutine(CreatePlayer());
    }

    void OnPlayerCreate() 
    {
        playerCreated?.Invoke();
    }

    IEnumerator CreatePlayer() 
    {
        // ���� ���� ��� �������� �����ð��� �ְ� ���� (Ȥ�� �� ���� ����)
        yield return new WaitForSeconds(1);

        // ���� �� �г��� 
        PhotonNetwork.NickName = "�ӽ��̸�";

        // �÷��̾� ���� 
        // PhotonNetwork�� Instantiate ��� 
        _player = PhotonNetwork.Instantiate("Player", new Vector3(0,0,0), Quaternion.identity);

        // �÷��̾ �����ϱ� �� ����
        // 1. Resources ������ "Player" ��� �̸��� �������� �־����
        // 2. ���� Ŭ���� ������ ����ȭ
        // : Photon View, Photon Transform , Photon Animator View ������Ʈ�� �����Ǿ� �־����

        // �÷���� ���� �Ϸ� ��������Ʈ ���� 
        OnPlayerCreate();
    }

    #region ���� ���� ����, ���� �κ� ���� �� �� (room) ����

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

    public override void OnConnectedToMaster() 
    {
        // MonoBehaviourPunCallbacks �� OnConnected~�Լ��� �������̵�
        // ���� �����Ͱ� �����ϸ� �޼��� ���� 
        Debug.Log("���濡 ���� ���� ���� : " + PhotonNetwork.IsConnected);

        // �κ� ���� �߰� 
        OnJoinedLobby();
    }

    public override void OnJoinedLobby()
    {
        // Photon���� Lobby�� �ǹ��ϴ°��� ?
        // ��Ƽ�÷��̾�� �÷��̾ �����ϱ� �� ������ ������
        // ���� ����(Master Server) -> �κ� ����(Lobby) -> �� ����/����(Room) -> ���� �� �ε�
        // �������� �� ���� ����� ���� 

        // �κ� �����ϸ� ����

        // Debug.Log("�κ����� "  + PhotonNetwork.InLobby);
        JointRoom();
    }

    private void JointRoom() 
    {
        // �����͸� �� ���� 
        // �� (room) �����ϴ� �Լ� ����
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        // ���� �� �ٷ� ����
        PhotonNetwork.JoinOrCreateRoom(roomName , roomOptions , TypedLobby.Default);
        // -> �� �̸� �������� �۵���
        // �� ������Ŭ���̾�Ʈ�� ���� �����, ���� �����ڴ� �̸��� �ش��ϴ� ���� �̹� ������ �ű�� ���� 
    }

    public override void OnCreatedRoom()
    {
        // �� ������ ȣ�� (�������̵�)
        Debug.Log("�� ���� �Ϸ� : " + PhotonNetwork.CurrentRoom.Name );
    }

    public override void OnJoinedRoom()
    {
        // �� ���� �� ȣ�� (�������̵�)
        Debug.Log($"{PhotonNetwork.CurrentRoom.Name} �� ���� / �濡 �ִ��� ���� : {PhotonNetwork.InRoom}" +
            $" �ο� �� : {PhotonNetwork.CurrentRoom.PlayerCount}" );
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // �� ���� ���� �� ȣ�� (�������̵�)
        Debug.Log($"JoinRoom Failed {returnCode} {message} / �� ���� -> �����õ���....");
        JointRoom();
    }
    #endregion
}
