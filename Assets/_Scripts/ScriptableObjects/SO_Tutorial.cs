using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialData", menuName = "ScriptableObjects/Tutorial Data", order = 1)]
public class SO_Tutorial : ScriptableObject
{
    public TutorialData[] Data;
}


[Serializable]
public class TutorialData
{
    public string Content;
    public Sprite Picture;
}