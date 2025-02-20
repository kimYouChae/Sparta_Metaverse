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

    [Header("===오브젝트===")]
    static public GameObject _videoChatObject;    // raw 이미지 프리팹
    static public Transform _videoChatLayout;     // 프리팹 담길 상위 부모 

    [Header("===아고라 인스턴스===")]
    static public IRtcEngine rtcEngine;

    protected override void Singleton_Awake()
    {

    }

    private void Start()
    {
        _videoChatObject = GameManager.Instnace._videoMangerChatObject;
        _videoChatLayout = GameManager.Instnace._videoMangerLayout;

        // rtc 엔진 생성, 초기화
        SetUpVideoRtcEngine();

        // 비디오 셋업
        SetVideoConfiguration();

        // 이벤트 핸들러 셋업
        InitEventHandler();
    }

    private void SetUpVideoRtcEngine()
    {
        // 아고라 인스턴스 생성
        rtcEngine = Agora.Rtc.RtcEngine.CreateAgoraRtcEngine();

        // 세팅
        RtcEngineContext context = new RtcEngineContext();
        context.appId = _appID;
        context.channelProfile = CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING;
        context.audioScenario = AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT;
        context.areaCode = AREA_CODE.AREA_CODE_GLOB;

        // rtc 엔진 초기화
        rtcEngine.Initialize(context);

        Debug.Log("아고라 엔진 셋업");
    }

    private void SetVideoConfiguration()
    {
        rtcEngine.EnableAudio();
        rtcEngine.EnableVideo();

        VideoEncoderConfiguration config = new VideoEncoderConfiguration();

        // 화면 영역 
        config.dimensions = new VideoDimensions(200, 200);
        config.frameRate = 15;
        config.bitrate = 1000;

        rtcEngine.SetVideoEncoderConfiguration(config);
        rtcEngine.SetChannelProfile(CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION);
        rtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);

        Debug.Log("아고라 비디오 셋업");

    }

    private void InitEventHandler()
    {
        UserEventHandler handler = new UserEventHandler(this);
        rtcEngine.InitEventHandler(handler);

        Debug.Log("아고라 유저 이벤트 핸들러 셋업");
    }

    internal class UserEventHandler : IRtcEngineEventHandler
    {
        readonly VideoChatManger _chatManger;

        internal UserEventHandler(VideoChatManger chatManger)
        {
            _chatManger = chatManger;
        }

        // 채널에 접속 성공시 출력 오버라이딩
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
            MakeVideoView(remoteUid, _chatManger._currentChannelName);
        }
        // 로컬 비디오 스트림 상태 변경 콜백
        public override void OnLocalVideoStateChanged(VIDEO_SOURCE_TYPE source, LOCAL_VIDEO_STREAM_STATE state, LOCAL_VIDEO_STREAM_REASON errorCode)
        {
            Debug.Log($"로컬 비디오 상태: {state}, 에러 코드: {errorCode}");

            switch (state)
            {
                case LOCAL_VIDEO_STREAM_STATE.LOCAL_VIDEO_STREAM_STATE_CAPTURING:
                    Debug.Log("카메라 캡처 중 - 카메라가 정상적으로 활성화되었습니다.");
                    break;
                case LOCAL_VIDEO_STREAM_STATE.LOCAL_VIDEO_STREAM_STATE_STOPPED:
                    Debug.Log("카메라 캡처가 중지되었습니다.");
                    break;
                case LOCAL_VIDEO_STREAM_STATE.LOCAL_VIDEO_STREAM_STATE_FAILED:
                    // 오류 코드별 상세 처리
                    if (errorCode == LOCAL_VIDEO_STREAM_REASON.LOCAL_VIDEO_STREAM_REASON_DEVICE_BUSY)
                    {
                        Debug.LogError("카메라 장치가 이미 사용 중입니다.");
                    }
                    else if (errorCode == LOCAL_VIDEO_STREAM_REASON.LOCAL_VIDEO_STREAM_REASON_DEVICE_NO_PERMISSION)
                    {
                        Debug.LogError("카메라 접근 권한이 없습니다.");
                    }
                    else
                    {
                        Debug.LogError($"카메라 캡처 실패! 상세 오류 코드: {errorCode}");
                    }
                    break;
            }
        }
    }

    // 해당 채널에 들어오면 
    public void Join(string channelName)
    {
        // 현재 채널 이름 저장 
        _currentChannelName = channelName;

        rtcEngine.JoinChannel(_token, channelName, 0, null);
        rtcEngine.EnableVideo();

        // 채널에 뷰 생성 
        string _cameraName = PlayerManager.Instnace.nowPlayer.PlayerName;
        MakeVideoView(0, channelName);

        Debug.Log($"{channelName} 의 채널에 조인 ");
    }

    // 해당 채널에서 나가면 
    public void Leave()
    {
        rtcEngine.DisableVideo();
        rtcEngine.LeaveChannel();

        // 생성된 뷰 삭제 
        DestoryAll();

        Debug.Log($"{_currentChannelName} 의 채널에 조인 ");
    }

    // 뷰 생성
    static public void MakeVideoView(uint uid, string channelId = "")
    {
        VideoSurface videoSurface = MakeImageSurface(uid.ToString());
        // null이면 return
        if (ReferenceEquals(videoSurface, null))
            return;

        // 로컬 , 리모트 유저 뷰 설정
        if (uid != 0)
        {
            videoSurface.SetForUser(uid, channelId);
        }
        else
        {
            videoSurface.SetForUser(uid, channelId, VIDEO_SOURCE_TYPE.VIDEO_SOURCE_CAMERA_PRIMARY);
        }

        videoSurface.SetEnable(true);

    }

    // 비디오 서페이스 생성
    static public VideoSurface MakeImageSurface(string name)
    {
        // raw 이미지 오브젝트 생성
        GameObject videochat = Instantiate(_videoChatObject);

        if (videochat == null)
            return null;

        // 오브젝트 이름 설정 
        videochat.name = name;

        if (_videoChatLayout != null)
        {
            // 비디오 레이아웃 밑에 넣기
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

        // 자식 오브젝트 모두 검색
        foreach(Transform child in parent) 
        { 
            result = FindChild(child, name);
            if (result != null) return result;
        }

        return null;


    }
}
