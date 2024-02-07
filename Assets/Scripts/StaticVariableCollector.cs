using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticVariableCollector
{
    public static MainManager.MainState mainState { get;private set; }

    public static DateTime gameTime { get; private set; }

    public static Vector3 mousePosition { get; private set; }

    public static void SetMainState(MainManager.MainState t_mainState)
    {
        if(mainState == t_mainState)
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

    static void EllegalStateInput()
    {
        Debug.LogError("不正なステート入力です");
    }

}
