using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup fadePanel;
    [SerializeField]
    private float animTime;

    private bool isFading = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        EventManager.SetFade += SetFade;
    }

    private void OnDestroy()
    {
        EventManager.SetFade -= SetFade;
    }

    private void SetFade(bool isFadeIn)
    {
        if (isFading) return;

        isFading = true;
        float a = isFadeIn ? 1f : 0f;

        LeanTween.alphaCanvas(fadePanel, a, animTime)
            .setEaseInOutSine()
            .setOnComplete(() =>
            {

                if (isFadeIn)
                {
                    EventManager.OnFadeInComplete?.Invoke();
                }
                else
                {
                    EventManager.OnFadeOutComplete?.Invoke();
                }

                isFading = false;
            });

    }

}
