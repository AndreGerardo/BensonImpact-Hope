
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bridge : MonoBehaviour
{
    private const float Alpha = 0.7f;
    
    [SerializeField] private TilemapCollider2D _collider;
    [SerializeField] private Tilemap _tilemap;

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
    }

    // Update is called once per frame
    void Update()
    {
        /*if(!_isAbleToMove) return;
        transform.position = Vector3.Lerp(transform.position,_currentTargetDestination , _speed * Time.deltaTime);
        
        
        if (Vector3.Distance(transform.position, _targetDestination) < 0.01f ) BridgeReady() ;*/
    }

    public void SetColor(Color currentColor)
    {
        _tilemap.color = new Color(currentColor.r,currentColor.g,currentColor.b,Alpha);
    }

    private void OnSoulUsed()
    {
        _tilemap.color =
            new Color(_tilemap.color.r, _tilemap.color.g, _tilemap.color.b, Alpha);
        _collider.enabled = true;
    }

    public void ActivateBridge()
    {
        _tilemap.color =
            new Color(_tilemap.color.r, _tilemap.color.g, _tilemap.color.b, 1f);
        _collider.enabled = false;

    }
    
    
}
