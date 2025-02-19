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
        // PhotonManger���� PhotonNetwork.Nickname�� �����߾��µ�
        // �׷� ���� photonNetwork�� ����� �̸��� PhotonView�� ����Ǵ°ǰ� ?
    }
}
