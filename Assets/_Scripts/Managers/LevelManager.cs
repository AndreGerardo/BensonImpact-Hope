using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        EventManager.OnNextLevel += NextLevel;
        EventManager.OnRestartLevel += RestartLevel;

    }

    private void OnDestroy()
    {
        EventManager.OnNextLevel -= NextLevel;
        EventManager.OnRestartLevel -= RestartLevel;
    }

    private void NextLevel()
    {
        EventManager.SetFade?.Invoke(true);

        print("NEXT LEVEL");

        LeanTween.delayedCall(1.5f, () =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }

    private void RestartLevel()
    {
        EventManager.SetFade?.Invoke(true);

        LeanTween.delayedCall(1.5f, () =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        
    }

}
