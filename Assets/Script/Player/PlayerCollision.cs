using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �̴ϰ���1 - Fluppy : ���� Ʈ����
        if (collision.gameObject.layer == LayerManger.Instnace.BlockLayerNum)
        {
            // playerHp������Ʈ 
            PlayerManager.Instnace.F_CollisionToBlcok();
        }

        // �̴ϰ��� Enterance�� Ʈ���� : �˾� on 
        if (collision.gameObject.layer == LayerManger.Instnace.GameEnteranceLayerNum)
        {
            UiManager.Instnace.F_OnOFfMiniGamePopUp(true);
        }

        // �̴ϰ��� ���� board : �˾� on
        if (collision.gameObject.layer == LayerManger.Instnace.GameScoreBoardLayerNum) 
        {
            UiManager.Instnace.F_OnOffScorePopUp(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �̴ϰ��� Enterance�� exit : �˾� off 
        if (collision.gameObject.layer == LayerManger.Instnace.GameEnteranceLayerNum)
        {
            UiManager.Instnace.F_OnOFfMiniGamePopUp(false);
        }
        // �̴ϰ��� ���� board : �˾� off
        if (collision.gameObject.layer == LayerManger.Instnace.GameScoreBoardLayerNum)
        {
            UiManager.Instnace.F_OnOffScorePopUp(false);
        }
    }
}
