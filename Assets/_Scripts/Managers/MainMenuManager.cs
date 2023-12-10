using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        EventManager.SetFade?.Invoke(false);
    }

    
    public void ButtonPlay()
    {
        EventManager.OnNextLevel?.Invoke();
    }

}
