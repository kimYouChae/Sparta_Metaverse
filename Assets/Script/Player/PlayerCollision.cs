using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 미니게임1 - Fluppy : 벽과 트리거
        if (collision.gameObject.layer == LayerManger.Instnace.BlockLayerNum)
        {
            // playerHp업데이트 
            PlayerManager.Instnace.F_CollisionToBlcok();
        }

        // 미니게임 Enterance랑 트리거 : 팝업 on 
        if (collision.gameObject.layer == LayerManger.Instnace.GameEnteranceLayerNum)
        {
            UiManager.Instnace.F_OnOFfMiniGamePopUp(true);
        }

        // 미니게임 점수 board : 팝업 on
        if (collision.gameObject.layer == LayerManger.Instnace.GameScoreBoardLayerNum) 
        {
            UiManager.Instnace.F_OnOffScorePopUp(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 미니게임 Enterance랑 exit : 팝업 off 
        if (collision.gameObject.layer == LayerManger.Instnace.GameEnteranceLayerNum)
        {
            UiManager.Instnace.F_OnOFfMiniGamePopUp(false);
        }
        // 미니게임 점수 board : 팝업 off
        if (collision.gameObject.layer == LayerManger.Instnace.GameScoreBoardLayerNum)
        {
            UiManager.Instnace.F_OnOffScorePopUp(false);
        }
    }
}
