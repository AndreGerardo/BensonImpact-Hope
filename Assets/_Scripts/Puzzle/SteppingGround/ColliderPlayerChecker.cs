using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPlayerChecker : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Ground _parentGround;
    private bool _isPlayerOnTrigger;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _isPlayerOnTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isPlayerOnTrigger = false;
        if(!_parentGround.GetIsActivated()) _parentGround.BridgeShutDown();
    }

    public bool GetPlayerOnTrigger()
    {
        return _isPlayerOnTrigger;
    }
}
