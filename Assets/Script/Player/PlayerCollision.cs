using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 미니게임1 - Fluppy : 벽과 충돌 
        if (collision.gameObject.layer == LayerManger.Instnace.BlockLayerNum)
        {
            Debug.Log("대충충돌");

            // playerHp업데이트 
            PlayerManager.Instnace.F_CollisionToBlcok();
        }
    }
}
