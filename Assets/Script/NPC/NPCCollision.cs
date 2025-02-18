using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCCollision : MonoBehaviour
{
    [SerializeField]
    private NPCScriptable _npcLines;

    [SerializeField]
    private TextMeshProUGUI _text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerManger.Instnace.PlayerLayerNum) 
        {
            if (_npcLines == null)
                return;

            int idx = Random.Range(0 , _npcLines.lines.Count);

            // npc 위 ui에 표시 
            _text.text = _npcLines.lines[idx];
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerManger.Instnace.PlayerLayerNum)
        {
            _text.text = "";
        }
    }
}
