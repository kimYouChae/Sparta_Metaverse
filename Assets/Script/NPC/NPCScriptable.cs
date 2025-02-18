using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Data", menuName = "Sciptable/NPC Data")]
public class NPCScriptable : MonoBehaviour
{
    [SerializeField]
    List<string> _lines;
}
