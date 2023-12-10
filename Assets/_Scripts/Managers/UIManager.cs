
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Win UI")] 
    [SerializeField] private GameObject _cnvsGameWin;

    [Space]
    [Header("Lose UI")] 
    [SerializeField] private GameObject _cnvsGameLose;
    [SerializeField] private Button _btnRestartLevel;

    [Header("Hope UI")]
    [SerializeField]
    private GameObject hopeIconPrefab;
    [SerializeField]
    private Transform hopeIconParent;
    
    private List<GameObject> hopeIconCollection = new List<GameObject>();

    [Header("Pause UI")]
    [SerializeField]
    private GameObject pausePanel;

    private bool isPauseOpen = false;

    private void Awake()
    {
        EventManager.OnPlayerWin += OnWinUI;
        EventManager.OnPlayerLose += OnLoseUI;
        EventManager.OnSetSoul += SetHopeUI;
        EventManager.OnSoulUsed += DecreaseHopeUI;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerWin -= OnWinUI;
        EventManager.OnPlayerLose -= OnLoseUI;
        EventManager.OnSetSoul -= SetHopeUI;
        EventManager.OnSoulUsed -= DecreaseHopeUI;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ButtonPauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.R) && isPauseOpen)
        {
            ButtonRestartLevel();
        }
    }

    public void OnWinUI()
    {
        EventManager.SetFade?.Invoke(true);
        LeanTween.delayedCall(1.5f, () =>
        {
            EventManager.OnNextLevel?.Invoke();

        });

    }

    public void OnLoseUI()
    {
        _cnvsGameLose.SetActive(true);
    }

    public void ButtonRestartLevel()
    {
        EventManager.OnRestartLevel?.Invoke();
    }

    public void ButtonPauseMenu()
    {
        isPauseOpen = !isPauseOpen;

        pausePanel.SetActive(isPauseOpen);

        EventManager.OnGamePause?.Invoke(isPauseOpen);

    }

    private void SetHopeUI(int count)
    {
        for (int i = 0; i < count-1; i++)
        {
            GameObject obj = Instantiate(hopeIconPrefab, hopeIconParent);

            hopeIconCollection.Add(obj);
        }
    }

    private void DecreaseHopeUI()
    {
        if (hopeIconCollection.Count <= 0) return;

        Destroy(hopeIconCollection[hopeIconCollection.Count - 1]);
        hopeIconCollection.RemoveAt(hopeIconCollection.Count - 1);

    }
}
