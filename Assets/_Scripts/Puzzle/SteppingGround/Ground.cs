using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Sprite[] _spriteList;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ColliderPlayerChecker _colliderPlayerChecker;
    private List<int> IDList;

    private bool _isStillActivated;

    private void Awake()
    {
        IDList = new List<int>();
        EventManager.OnSoulUsed += OnSoulUsed;
        _spriteRenderer.sprite = _spriteList[0];
    }

    private void OnSoulUsed()
    {
        IDList.Clear();

    }

    private void OnDestroy()
    {
        EventManager.OnSoulUsed -= OnSoulUsed;
    }


    public void ActivateBridge(int ID)
    {
        if(CheckIsActivatedDouble(ID))return;
        _isStillActivated = true;
        _spriteRenderer.sprite = _spriteList[1];
        IDList.Add(ID);
        _collider.isTrigger = true;
    }

    public void DeactivateBridge(int ID)
    {
        _isStillActivated = false;
        IDList.Remove(ID);
        
        if(!_colliderPlayerChecker.GetPlayerOnTrigger())BridgeShutDown();
    }

    public void BridgeShutDown()
    {

        _collider.isTrigger = false;
        _spriteRenderer.sprite = _spriteList[0];
    }




    private bool CheckIsActivatedDouble(int id)
    {
        foreach (var data in IDList)
        {
            if(data != id) continue;
            return true;
        }

        return false;
    }

    public bool GetIsActivated()
    {
        return _isStillActivated;
    }

    
}
