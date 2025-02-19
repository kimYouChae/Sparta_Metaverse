using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConferenceBox : MonoBehaviour
{
    [SerializeField]
    private string _chanelName = "ConferenceRoom";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ ������ 
        if (collision.gameObject.layer == LayerManger.Instnace.PlayerLayerNum) 
        {
            // Ui �ѱ�
            UiManager.Instnace._conferenceCameraPanel.SetActive(true);

            // Agora - ����
            VideoChatManger.Instnace.Join(_chanelName);        
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �÷��̾ ������ 
        if (collision.gameObject.layer == LayerManger.Instnace.PlayerLayerNum)
        {
            // Ui ����
            UiManager.Instnace._conferenceCameraPanel.SetActive(false);

            // Agora - ������
            VideoChatManger.Instnace.Leave();
        }
    }
}
