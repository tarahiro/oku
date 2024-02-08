using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticVariableCollector
{
    public static MainManager.MainState mainState { get;private set; }

    public static DateTime gameTime { get; private set; }

    public static Vector3 mousePosition { get; private set; }

    public static PlayerInputReciever.TTouchState tTouchState { get; private set; }

    public static void SetMainState(MainManager.MainState t_mainState)
    {
        if(mainState == t_mainState && mainState != MainManager.MainState.None)
        {
            EllegalStateInput();
        }
        mainState = t_mainState;
    }

    public static void SetGameTime(DateTime t_gameTime)
    {
        gameTime = t_gameTime;
    }

    public static void SetMousePosition(Vector3 t_mousePosition)
    {
        mousePosition = t_mousePosition;
    }

    public static void SetTTouchState(PlayerInputReciever.TTouchState t_tTouchState)
    {
        tTouchState = t_tTouchState;
    }

    static void EllegalStateInput()
    {
        Debug.LogError("不正なステート入力です");
    }

}
