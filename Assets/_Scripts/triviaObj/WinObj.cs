using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinObj : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Win Game Detect");
            EventManager.OnPlayerWin?.Invoke();

            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
