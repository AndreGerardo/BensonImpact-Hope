using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPuzzleElement : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Sprite[] _spriteList;
    private FloorPuzzleManager _parentManager;
    private bool _isActive;
    private bool _isSolved;
    private bool _isStepped;

    private Color _activeColor;

    private void Awake()
    {
        EventManager.OnSoulUsed += OnPuzzleRestart;
        
    }

    private void OnDestroy()
    {
        EventManager.OnSoulUsed -= OnPuzzleRestart;
    }

    public void OnPuzzleRestart()
    {
        _isSolved = false;
       // _sprite.color = Color.yellow;
       _sprite.sprite = _spriteList[0];
       _sprite.color = Color.white;
        _isActive = false;
    }

    public void SetColor(Color activeColor)
    {
        _activeColor = activeColor;
    }

    public void SetParentManager(FloorPuzzleManager parent)
    {
        _parentManager = parent;
    }

    public void OnPuzzleSolved()
    {
        _isSolved = true;
        _sprite.color = _activeColor;
        _isActive = false;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !_isStepped)
        {
            if(_isActive)_parentManager.OnPuzzleRestart();
            
            
            // can be changed
            _sprite.color = _activeColor;
            _sprite.sprite = _spriteList[1];
            _isStepped = true;
            _isActive = true;
            _parentManager.AddActiveFloor(1);
            
            if(!_isSolved)
            EventManager.OnSoundPlayOnce?.Invoke(7);
            
            StartCoroutine(DurationCountdown());
        }    
    }

    private IEnumerator DurationCountdown()
    {
        yield return new WaitForSeconds(0.01f);
        _isStepped = false;
        
        yield return new WaitForSeconds(_duration);
        if (!_isSolved)
        {
            //_sprite.color = Color.yellow;
            _parentManager.AddActiveFloor(-1);
            _isActive = false;
            _sprite.sprite = _spriteList[0];
            _sprite.color = Color.white;
        }
        
        
    }
    
    
    
}
