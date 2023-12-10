using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDeadAnim : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void SetDeathAnim(bool isPlayer)
    {
        string animTrigger = isPlayer ? "PlayerDie" : "HopeDie";

        _anim.SetTrigger(animTrigger);

        LeanTween.delayedCall(1.5f, () =>
        {
            EventManager.OnResetPlayer.Invoke();
            Destroy(gameObject);
        });
    }

}
