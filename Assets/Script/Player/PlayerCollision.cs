using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �̴ϰ���1 - Fluppy : ���� �浹 
        if (collision.gameObject.layer == LayerManger.Instnace.BlockLayerNum)
        {
            Debug.Log("�����浹");

            // playerHp������Ʈ 
            PlayerManager.Instnace.F_CollisionToBlcok();
        }
    }
}
