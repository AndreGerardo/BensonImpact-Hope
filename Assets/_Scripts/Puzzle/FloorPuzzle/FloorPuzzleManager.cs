using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorPuzzleManager : MonoBehaviour
{
    [SerializeField] private Bridge _bridge;
    [SerializeField] private Color _bridgeColor;
    
    private int _activeFloorCount;
    private List<FloorPuzzleElement> _floors;

    

    private FloorPuzzleElement _tempFloor;
    private Action _onPuzzleSolved;
    private Action _onPuzzleReset;

    private bool _isSolved;

    // Start is called before the first frame update
    private void Awake()
    {
        _floors = new List<FloorPuzzleElement>();
        foreach (Transform data in transform)
        {
            _tempFloor = data.GetComponent<FloorPuzzleElement>();
            _tempFloor.SetParentManager(this);
            _tempFloor.SetColor(_bridgeColor);
            _onPuzzleSolved += _tempFloor.OnPuzzleSolved;
            _onPuzzleReset += _tempFloor.OnPuzzleRestart;
            
            _floors.Add(_tempFloor);
        }
        _bridge.SetColor(_bridgeColor);
        EventManager.OnSoulUsed += OnSoulUsed;
    }

    private void OnDestroy()
    {
        EventManager.OnSoulUsed -= OnSoulUsed;
    }

    private void OnSoulUsed()
    {
        _activeFloorCount = 0;
        _isSolved = false;
    }


    public void AddActiveFloor(int number)
    {
        _activeFloorCount += number;
        if (_activeFloorCount < 0) _activeFloorCount = 0;
        CheckComplete();
    }
    
    private void CheckComplete()
    {
        if (_activeFloorCount >= _floors.Count)
        {
            
            _onPuzzleSolved?.Invoke();
            _bridge.ActivateBridge();
            
            if(!_isSolved) EventManager.OnSoundPlayOnce?.Invoke(9);
            _isSolved = true;
        }
    }

    public void OnPuzzleRestart()
    {
        _onPuzzleReset?.Invoke();
        _activeFloorCount = 0;
    }
}
