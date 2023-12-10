using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSequence : MonoBehaviour
{
    [SerializeField]
    private GameObject EndingBG;
    [SerializeField]
    private GameObject DialogueCanvas;

    [SerializeField]
    private List<GameObject> SequenceList = new List<GameObject>();
    private int currentSequence = 0;

    [SerializeField]
    private SO_Dialogue dialogue;

    private void Awake()
    {
        EventManager.OnSentenceDialogueEnd += ChangeSequence;
        //EventManager.OnDialogueEnd += Ending;
    }

    private void OnDestroy()
    {
        EventManager.OnSentenceDialogueEnd -= ChangeSequence;
        //EventManager.OnDialogueEnd -= Ending;
    }

    private void Start()
    {
        EventManager.SetFade?.Invoke(false);
        DialogueCanvas.SetActive(true);
        EventManager.OnDialogueStart?.Invoke(dialogue);
        LeanTween.delayedCall(1.5f, () =>
        {
            EventManager.OnBirdSound?.Invoke();
        });
    }

    private void ChangeSequence()
    {
        //EventManager.SetFade?.Invoke(true);
        LeanTween.delayedCall(1.25f, () =>
        {
            //EventManager.SetFade?.Invoke(false);
            if (currentSequence != SequenceList.Count - 1)
            {
                SequenceList[currentSequence].SetActive(false);
            } 
            currentSequence++;
            if (currentSequence < SequenceList.Count)
            {
                SequenceList[currentSequence].SetActive(true);
                return;
            }

            EventManager.SetFade?.Invoke(true);
            LeanTween.delayedCall(1.5f, () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            });

        });

    }

    private void Ending()
    {
        EventManager.StopBirdSound?.Invoke();

        SequenceList[currentSequence].SetActive(true);
        LeanTween.delayedCall(3f, () =>
        {
            EventManager.SetFade?.Invoke(true);
            LeanTween.delayedCall(1.5f, () =>
            {
                SceneManager.LoadScene(0);
            });
        });
    }
}
