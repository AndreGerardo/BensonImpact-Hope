using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSequence : MonoBehaviour
{

    [SerializeField]
    private GameObject OpeningBG;
    [SerializeField]
    private GameObject DialogueCanvas;

    [SerializeField]
    private List<GameObject> SequenceList = new List<GameObject>();
    private int currentSequence = 0;

    [SerializeField]
    private SO_Dialogue dialogue;

    private void Awake()
    {
        EventManager.OnDialogueEnd += ChangeSequence;
    }

    private void OnDestroy()
    {
        EventManager.OnDialogueEnd -= ChangeSequence;
    }

    private void Start()
    {
        EventManager.SetFade?.Invoke(false);

        float currentDelay = 6f;
        LeanTween.delayedCall(currentDelay, () =>
        {
            EventManager.SetFade?.Invoke(true);
        });

        LeanTween.delayedCall(8f, () =>
        {
            EventManager.SetFade?.Invoke(false);
            OpeningBG.SetActive(false);
            DialogueCanvas.SetActive(true);
            EventManager.OnDialogueStart?.Invoke(dialogue);
            
        });
    }

    private void ChangeSequence()
    {
        EventManager.SetFade?.Invoke(true);
        LeanTween.delayedCall(1.5f, () =>
        {
            SequenceList[currentSequence].SetActive(true);
            EventManager.SetFade?.Invoke(false);
            currentSequence++;
        });

        LeanTween.delayedCall(3f, () =>
        {
            EventManager.SetFade?.Invoke(true);
            LeanTween.delayedCall(1.5f, () =>
            {
                EventManager.OnNextLevel?.Invoke();
            });
        });

    }
}
