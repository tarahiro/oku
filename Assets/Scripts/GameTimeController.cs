using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    [SerializeField] GameTimeCounter m_counter;


    MainManager m_mainManagerCache;
    ReportFactory m_reportFactory;
    Mental m_mental;
    [SerializeField] GameTimeView m_gameTimeView;
    [SerializeField] ReportController reportController;

    private void Awake()
    {
        m_mainManagerCache = FindObjectOfType<MainManager>();
        m_reportFactory = FindObjectOfType<ReportFactory>();
        m_mental = FindObjectOfType<Mental>();
    }

    public void UpdateDateTime(int ProgressTickCount)
    {

        //ステートによらない共通処理
        UpdateCommonGameTime();


        //ステート依存の処理
        switch (StaticVariableCollector.mainState)
        {
            case MainManager.MainState.None:
                m_mainManagerCache.JudgeState();
                break;

            case MainManager.MainState.Rest:
                m_mental.RestoreMental(ProgressTickCount);
                m_mainManagerCache.JudgeState();
                break;

            case MainManager.MainState.Report:
                reportController.ProgressReport(ProgressTickCount);
                break;

            case MainManager.MainState.Savotage:
                m_mental.RestoreMental(ProgressTickCount);
                break;

            case MainManager.MainState.Exhausted:
                m_mental.RestoreExhaust(ProgressTickCount);
                break;


        }

        CheckSituation();
    }

    void UpdateCommonGameTime()
    {
        m_gameTimeView.UpdateTime(StaticVariableCollector.gameTime);
        m_reportFactory.ReportDataCheck();
    }

    void CheckSituation()
    {
        reportController.CheckDeadLine();
    }
  
}
