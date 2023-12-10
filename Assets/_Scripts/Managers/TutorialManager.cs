using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private SO_Tutorial _tutorialData;
    [Header("UI")]
    [SerializeField] private GameObject _cnvsTutorial;
    
    [SerializeField] private TextMeshProUGUI _txtContent;

    [SerializeField] private Image _imgTutorial;

    [SerializeField] private Button _btnNext;

    private int index;

    private void Awake()
    {
        _btnNext.onClick.AddListener(NextTutorial);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpTutorial()
    {
        _cnvsTutorial.SetActive(true);
        index = 0;
        NextTutorial();
    }

    private void NextTutorial()
    {
        if (index < _tutorialData.Data.Length)
        {
            _txtContent.SetText(_tutorialData.Data[index].Content);
            _imgTutorial.sprite = _tutorialData.Data[index].Picture;
            index++;
        }
        else
        {
            EndTutorial();
        }
        
    }

    private void EndTutorial()
    {
        _cnvsTutorial.SetActive(false);
    }
    
}
