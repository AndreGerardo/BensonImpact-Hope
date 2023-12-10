
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    
    [SerializeField] private GameObject _gateGO;
    
    [Header("set up parameter")]
    [SerializeField] private Vector3 _targetLocation;
    [SerializeField] private float _speed;
    
    private bool _isAbleToMove;
    private Vector3 _firstLocation;
    private Vector3 _currentTarget;


    private void Awake()
    {
        _firstLocation = transform.localPosition;
    }


    private void Update()
    {
        if(!_isAbleToMove) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, _currentTarget, _speed * Time.deltaTime);
        if (Vector3.Distance(transform.localPosition, _currentTarget) <0.01f) _isAbleToMove = false;
    }


    public void OpenGate()
    {
        _isAbleToMove = true;
        _currentTarget = _targetLocation;
        
        EventManager.OnSoundPlayOnce?.Invoke(8);
        
    }
    
    

    public void CloseGate()
    {
        _isAbleToMove = true;
        _currentTarget = _firstLocation;
        EventManager.OnSoundPlayOnce?.Invoke(8);
    }



}
