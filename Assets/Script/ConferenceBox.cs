using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConferenceBox : MonoBehaviour
{
    [SerializeField]
    private string _chanelName = "ConferenceRoom";

    // 플레이어의 포톤 정보 가져오기 
    [SerializeField]
    private PhotonView _playerView;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerView = collision.GetComponent<PhotonView>();

        // 플레이어가 들어오면 + 로컬 플레이어면 
        if (collision.gameObject.layer == LayerManger.Instnace.PlayerLayerNum
            && _playerView.IsMine ) 
        {
            // Ui 켜기
            UiManager.Instnace._conferenceCameraPanel.SetActive(true);

            // Agora - 조인
            VideoChatManger.Instnace.Join(_chanelName);        
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerView = collision.GetComponent<PhotonView>();

        // 플레이어가 들어오면 + 로컬플레이어면
        if (collision.gameObject.layer == LayerManger.Instnace.PlayerLayerNum
            && _playerView.IsMine)
        {
            // Ui 끄기
            UiManager.Instnace._conferenceCameraPanel.SetActive(false);

            // Agora - 떠나기
            VideoChatManger.Instnace.Leave();
        }
    }
}
