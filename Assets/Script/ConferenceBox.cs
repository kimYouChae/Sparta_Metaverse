using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConferenceBox : MonoBehaviour
{
    [SerializeField]
    private string _chanelName = "ConferenceRoom";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어가 들어오면 
        if (collision.gameObject.layer == LayerManger.Instnace.PlayerLayerNum) 
        {
            // Ui 켜기
            UiManager.Instnace._conferenceCameraPanel.SetActive(true);

            // Agora - 조인
            VideoChatManger.Instnace.Join(_chanelName);        
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어가 들어오면 
        if (collision.gameObject.layer == LayerManger.Instnace.PlayerLayerNum)
        {
            // Ui 끄기
            UiManager.Instnace._conferenceCameraPanel.SetActive(false);

            // Agora - 떠나기
            VideoChatManger.Instnace.Leave();
        }
    }
}
