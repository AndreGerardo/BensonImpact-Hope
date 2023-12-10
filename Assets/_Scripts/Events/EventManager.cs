using System;
using UnityEngine;

public static partial class EventManager
{
    public static Action<int> OnSetSoul;

    public static Action OnResetPlayer;

    public static Action<bool> SetFade;
    public static Action OnFadeInComplete;
    public static Action OnFadeOutComplete;

    public static Action<bool> OnGamePause;

    public static Action<SO_Dialogue> OnDialogueStart;
    public static Action OnDialogueEnd;
    public static Action OnSentenceDialogueEnd;
}
