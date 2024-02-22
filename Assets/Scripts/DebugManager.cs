using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    TimeController timeController;
    ReportController reportController;
    ReportFactory reportFactory;

    List<DateTime> debugDateTimeList = new List<DateTime>()
    {
        new DateTime(2024, 4, 5),
        new DateTime(2024, 4, 14),
        new DateTime(2024, 4, 23),
        new DateTime(2024,5,6)
    };

    private void Awake()
    {
        timeController = FindObjectOfType<TimeController>();
        reportController = FindObjectOfType<ReportController>();
        reportFactory = FindObjectOfType<ReportFactory>();
    }

    public void SkipToNextDay()
    {
        timeController.GoToNextDay();
    }

    //特定の日付に移動。レポートはすべてクリアにする
    public void SkipToSpecificDayWithoutReport()
    {
        for (int i = 0; i < debugDateTimeList.Count; i++)
        {
            if (StaticVariableCollector.gameTime < debugDateTimeList[i])
            {
                reportController.ForceAllReportClear();
                reportFactory.ForceReportSet(debugDateTimeList[i]);
                timeController.GoToSpecificDay(debugDateTimeList[i]);
                break;
            }
        }
    }
}
