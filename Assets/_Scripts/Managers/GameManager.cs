using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _maxSoul;

    private int _currentSoul;

    private void Awake()
    {
        EventManager.OnSoulUsed += OnSoulUsed;
    }

    private void OnDestroy()
    {
        EventManager.OnSoulUsed -= OnSoulUsed;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        SetUpGame();

        EventManager.SetFade?.Invoke(false);

    }

    private void SetUpGame()
    {
        _currentSoul = _maxSoul;
        EventManager.OnSetSoul?.Invoke(_maxSoul);
    }


    private void OnSoulUsed()
    {
        _currentSoul -= 1;
        if (_currentSoul <= 0)
        {
            EventManager.OnPlayerLose?.Invoke();
            Debug.Log("Lose Game");
        }
    }
    
    
    
}
