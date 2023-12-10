using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HopeSequence : MonoBehaviour
{

    void Start()
    {
        EventManager.SetFade?.Invoke(false);
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
