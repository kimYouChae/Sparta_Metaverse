using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agora.Rtc;

public class VideoChatManger : Singleton<VideoChatManger>
{
    [Header("===Data===")]
    [SerializeField] private string _appID = "85636c045ba54ceda8cee9c505f35e5b";
    [SerializeField] private string _token = "";
    [SerializeField] private string _currentChannelName;

    [Header("===������Ʈ===")]
    [SerializeField]
    static public GameObject _videoChatObject;    // raw �̹��� ������
    [SerializeField]
    static public Transform _videoChatLayout;     // ������ ��� ���� �θ� 

    [Header("===�ư�� �ν��Ͻ�===")]
    static public IRtcEngine rtcEngine;

    protected override void Singleton_Awake()
    {

    }

    private void Start()
    {
        // rtc ���� ����, �ʱ�ȭ
        SetUpVideoRtcEngine();

        // ���� �¾�
        SetVideoConfiguration();

        // �̺�Ʈ �ڵ鷯 �¾�
        InitEventHandler();
    }

    private void SetUpVideoRtcEngine()
    {
        // �ư�� �ν��Ͻ� ����
        rtcEngine = Agora.Rtc.RtcEngine.CreateAgoraRtcEngine();

        // ����
        RtcEngineContext context = new RtcEngineContext();
        context.appId = _appID;
        context.channelProfile = CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING;
        context.audioScenario = AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT;
        context.areaCode = AREA_CODE.AREA_CODE_GLOB;

        // rtc ���� �ʱ�ȭ
        rtcEngine.Initialize(context);

        Debug.Log("�ư�� ���� �¾�");
    }

    private void SetVideoConfiguration()
    {
        rtcEngine.EnableAudio();
        rtcEngine.EnableVideo();

        VideoEncoderConfiguration config = new VideoEncoderConfiguration();

        // ȭ�� ���� 
        config.dimensions = new VideoDimensions(200, 200);
        config.frameRate = 15;
        config.bitrate = 0;

        rtcEngine.SetVideoEncoderConfiguration(config);
        rtcEngine.SetChannelProfile(CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION);
        rtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);

        Debug.Log("�ư�� ���� �¾�");

    }

    private void InitEventHandler()
    {
        UserEventHandler handler = new UserEventHandler(this);
        rtcEngine.InitEventHandler(handler);

        Debug.Log("�ư�� ���� �̺�Ʈ �ڵ鷯 �¾�");
    }

    internal class UserEventHandler : IRtcEngineEventHandler
    {
        readonly VideoChatManger _chatManger;

        internal UserEventHandler(VideoChatManger chatManger)
        {
            _chatManger = chatManger;
        }

        // ä�ο� ���� ������ ��� �������̵�
        public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed)
        {
            Debug.Log($"You Joined Channel : " + connection.channelId);
        }

        public override void OnUserOffline(RtcConnection connection, uint remoteUid, USER_OFFLINE_REASON_TYPE reason)
        {
            DestoryVideoView(remoteUid);
        }
        public override void OnUserJoined(RtcConnection connection, uint remoteUid, int elapsed)
        {

        }
    }

    // �ش� ä�ο� ������ 
    public void Join(string channelName)
    {
        // ���� ä�� �̸� ���� 
        _currentChannelName = channelName;

        rtcEngine.JoinChannel(_token, channelName, 0, null);
        rtcEngine.EnableVideo();

        // ä�ο� �� ���� 
        MakeVideoView(0, channelName);

        Debug.Log($"{channelName} �� ä�ο� ���� ");
    }

    // �ش� ä�ο��� ������ 
    public void Leave()
    {
        rtcEngine.LeaveChannel();
        rtcEngine.DisableVideo();

        // ������ �� ���� 
        DestoryAll();

        Debug.Log($"{_currentChannelName} �� ä�ο� ���� ");
    }

    // �� ����
    static public void MakeVideoView(uint uid, string channelId = "")
    {
        VideoSurface videoSurface = MakeImageSurface(uid.ToString());
        // null�̸� return
        if (ReferenceEquals(videoSurface, null))
            return;

        // ���� , ����Ʈ ���� �� ����
        if (uid != 0)
        {
            videoSurface.SetForUser(uid, channelId);
        }
        else
        {
            videoSurface.SetForUser(uid, channelId, VIDEO_SOURCE_TYPE.VIDEO_SOURCE_REMOTE);
        }

        videoSurface.SetEnable(true);

    }

    // ���� �����̽� ����
    static public VideoSurface MakeImageSurface(string name)
    {
        // raw �̹��� ������Ʈ ����
        GameObject videochat = Instantiate(_videoChatObject);

        if (videochat == null)
            return null;

        // ������Ʈ �̸� ���� 
        videochat.name = name;

        if (_videoChatLayout != null)
        {
            // ���� ���̾ƿ� �ؿ� �ֱ�
            videochat.transform.SetParent(_videoChatLayout, false);
        }
        else
        {
            Debug.Log("Layout is null");
        }

        videochat.transform.localPosition = Vector3.zero;
        var videoSurface = videochat.AddComponent<VideoSurface>();

        return videoSurface;
    }

    static public void DestoryAll()
    {
        List<uint> uids = new List<uint>();

        for (int i = 0; i < _videoChatLayout.transform.childCount; i++)
        {
            uids.Add(uint.Parse(_videoChatLayout.transform.GetChild(i).name));
        }

        for (int i = 0; i < uids.Count; i++)
        {
            DestoryVideoView(uids[i]);
        }
    }

    static public void DestoryVideoView(uint id) 
    {
        var obj = FindChild(UiManager.Instnace._canvas.transform, id.ToString()).gameObject;

        if (!ReferenceEquals(obj, null))
            Destroy(obj);
    }

    static public Transform FindChild(Transform parent, string name)
    {
        Transform result = parent.Find(name);

        if (result != null)
            return result;

        // �ڽ� ������Ʈ ��� �˻�
        foreach(Transform child in parent) 
        { 
            result = FindChild(child, name);
            if (result != null) return result;
        }

        return null;


    }
}
