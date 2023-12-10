using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class SteppingGroundManager : MonoBehaviour
{
    [SerializeField] private List<PlatformGroundData> PlatformAndGroundData;

    private PlatformSteppingGround _currentPlatform;
    private Ground _currentGround;
    
    private Action _onPuzzleReset;
    
    private void Awake()
    {

        for (int i = 0; i < PlatformAndGroundData.Count; i++)
        {
            
            PlatformAndGroundData[i].PlatformData.SetID(i);
            PlatformAndGroundData[i].PlatformData.SetParentManager(this);
            PlatformAndGroundData[i].PlatformData.SetUpConnectionGround(PlatformAndGroundData[i].GroundData);
            _onPuzzleReset += PlatformAndGroundData[i].PlatformData.OnPuzzleRestart;
            
        }
        
        
        
    }
}
[Serializable]
public class PlatformGroundData
{
    public PlatformSteppingGround PlatformData;
    public List<Ground> GroundData;
}
