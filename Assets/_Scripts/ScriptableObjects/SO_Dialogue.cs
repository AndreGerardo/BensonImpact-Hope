
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "ScriptableObjects/Dialogue Data", order = 1)]
public class SO_Dialogue : ScriptableObject
{
    public DialogueData[] Data;
}


[Serializable]
public class DialogueData
{
    public string ActorName;
    public ActorType Type;
    [TextArea(15,20)]
    public string Dialogue;
    
}

public enum ActorType
{
    Player,
    NPC
}