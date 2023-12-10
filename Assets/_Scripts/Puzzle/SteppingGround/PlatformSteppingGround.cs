using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformSteppingGround : MonoBehaviour
{
    
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Sprite[] _spriteList;
    private List<Ground> _groundList;
    private int ID;
    private SteppingGroundManager _parentManager;
    private int count;

    public void SetID(int id)
    {
        ID = id;
    }
    
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
        // _sprite.color = Color.yellow;
        _sprite.sprite = _spriteList[0];
        _sprite.color = Color.white;
    }

    public void SetUpConnectionGround(List<Ground> data)
    {
        _groundList = data;
    }
    

    public void SetParentManager(SteppingGroundManager parent)
    {
        _parentManager = parent;
    }
    


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            foreach (var data in _groundList)
            {
                data.ActivateBridge(ID);
                EventManager.OnSoundPlayOnce?.Invoke(7);
            }

            _sprite.sprite = _spriteList[1];
            count++;
        }    
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") )
        {
            count--;
            if (count <= 0)
            {
                count = 0;
                foreach (var data in _groundList)
                {
                    data.DeactivateBridge(ID);
                }
                _sprite.sprite = _spriteList[0];
            }
            
        }  
    }
}
