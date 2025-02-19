using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerNickName : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;

    private void Start()
    {
        _name.text = GetComponent<PhotonView>().Owner.NickName;

        // ## TODO 
        // PhotonManger에서 PhotonNetwork.Nickname을 설정했었는데
        // 그럼 현재 photonNetwork에 저장된 이름이 PhotonView에 저장되는건가 ?
    }
}
