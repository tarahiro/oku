using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    [SerializeField] GameTimeCounter m_counter;

    DateTime StartDateTime = new DateTime(2024, 4, 1, 10, 0, 0);

    MainManager m_mainManagerCache;
    ReportFactory m_reportFactory;
    [SerializeField] GameTimeView m_gameTimeView;
    [SerializeField] ReportController reportController;

    private void Awake()
    {
        m_mainManagerCache = FindObjectOfType<MainManager>();
        m_reportFactory = FindObjectOfType<ReportFactory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_counter.Set(StartDateTime,this);
    }

    public void UpdateDateTime(int ProgressTickCount)
    {

        //ステートによらない共通処理
        UpdateCommonGameTime();


        //ステート依存の処理
        switch (m_mainManagerCache.mainState)
        {
            case MainManager.MainState.None:
                m_mainManagerCache.JudgeState();
                break;

            case MainManager.MainState.Report:
                reportController.ProgressReport(ProgressTickCount);
                break;

        }
    }

    void UpdateCommonGameTime()
    {
        m_gameTimeView.UpdateTime(m_mainManagerCache.gameTime);
        m_reportFactory.ReportDataCheck();
    }
  
}
