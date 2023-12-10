using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
    [SerializeField] private Gate _gate;
    [SerializeField] private Sprite[] _gateStateSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private bool _isSessionOpened;
    private int count;



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") )
        {
            _spriteRenderer.sprite = _gateStateSprite[1];
            _gate.OpenGate();
            EventManager.OnSoundPlayOnce?.Invoke(7);
            count++;
        }
            
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        
        if (col.CompareTag("Player"))
        {
            count--;
            if (count < 0) count = 0;

            if (count <= 0)
            {
                _gate.CloseGate();
                _spriteRenderer.sprite = _gateStateSprite[0];
            }


        }
    }
}
