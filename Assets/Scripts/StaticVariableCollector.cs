using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticVariableCollector
{
    static MainManager mainManager = null;

    public static MainManager.MainState mainState
    {
        get
        {
            if (mainManager == null)
            {
                mainManager = GameObject.FindObjectOfType<MainManager>();
            }
            return mainManager.mainState;
        }
    }

    public static DateTime gameTime { get; private set; }

    public static Vector3 mousePosition { get; private set; }

    public static PlayerInputReciever.TTouchState tTouchState { get; private set; }

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


    static ReportMasterDataList m_reportMasterDataList = null;
    public static ReportMasterDataList GetReportMasterDataList()
    {
        if(m_reportMasterDataList == null)
        {
            m_reportMasterDataList = Resources.Load<ReportMasterDataList>("Data/Report");
        }
        return m_reportMasterDataList;
    }

}
