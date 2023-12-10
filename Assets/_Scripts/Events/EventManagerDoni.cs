using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class EventManager
{
    public static Action OnPlayerWin;
    public static Action OnPlayerLose;
    public static Action OnRestartLevel;
    public static Action OnNextLevel;
    public static Action OnSoulUsed;

    #region Sound

    public static Action<int> OnSoundPlayOnce;
    public static Action<int> OnBGMPlay;
    public static Action OnFootStep;
    public static Action OnBirdSound;
    public static Action StopBirdSound;

    #endregion


}
