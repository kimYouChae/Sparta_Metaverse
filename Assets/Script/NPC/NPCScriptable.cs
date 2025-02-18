using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Data", menuName = "Sciptable/NPC Data")]
public class NPCScriptable : ScriptableObject
{
    [SerializeField]
    List<string> _lines;

    public List<string> lines => _lines;    
}
