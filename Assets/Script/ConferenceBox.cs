using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConferenceBox : MonoBehaviour
{
    [SerializeField]
    private string _chanelName = "ConferenceRoom";

    // �÷��̾��� ���� ���� �������� 
    [SerializeField]
    private PhotonView _playerView;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerView = collision.GetComponent<PhotonView>();

        // �÷��̾ ������ + ���� �÷��̾�� 
        if (collision.gameObject.layer == LayerManger.Instnace.PlayerLayerNum
            && _playerView.IsMine ) 
        {
            // Ui �ѱ�
            UiManager.Instnace._conferenceCameraPanel.SetActive(true);

            // Agora - ����
            VideoChatManger.Instnace.Join(_chanelName);        
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerView = collision.GetComponent<PhotonView>();

        // �÷��̾ ������ + �����÷��̾��
        if (collision.gameObject.layer == LayerManger.Instnace.PlayerLayerNum
            && _playerView.IsMine)
        {
            // Ui ����
            UiManager.Instnace._conferenceCameraPanel.SetActive(false);

            // Agora - ������
            VideoChatManger.Instnace.Leave();
        }
    }
}
