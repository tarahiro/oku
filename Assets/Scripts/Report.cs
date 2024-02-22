using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Report : MonoBehaviour
{

    int m_clearTick;
    string m_reportName;
    DateTime m_deadLine;
    public DateTime calculateDeadLine { get;private set; }
    public bool isFinished { get; private set; } = false;

    int m_currentTick = 0;
    ReportControllerView reportControllerViewCache;
    PcDisplayView pcDisplayViewCache;
    ReportView reportViewCache;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetReport(string reportName, DateTime deadLine, int clearTick, int colorId,
        ReportControllerView reportControllerView, PcDisplayView pcDisplayView)
    {
        m_reportName = reportName;
        m_deadLine = deadLine;
        calculateDeadLine = m_deadLine + new TimeSpan(1,0,0,0);
        m_clearTick = clearTick;
        reportControllerViewCache = reportControllerView;
        pcDisplayViewCache = pcDisplayView;

        reportViewCache = reportControllerView.AddReport(m_reportName, m_deadLine,colorId);
    }

    public void Finish()
    {
        isFinished = true;
        pcDisplayViewCache.FinishReport(m_reportName);
    }

    public void Clear()
    {
        reportViewCache.Clear();
        pcDisplayViewCache.ClearReport();
        SoundManager.PlaySE(SoundManager.SELabel.Enter);
        Destroy(this);
    }

    public void StartReport()
    {
        pcDisplayViewCache.StartReport(m_reportName, m_currentTick, m_clearTick);
    }

    public bool IsClearReport(int ProgressTickCount)
    {
        m_currentTick += ProgressTickCount;
        pcDisplayViewCache.ProgressReport(m_currentTick, m_clearTick);
        reportViewCache.UpdateReportView(m_currentTick, m_clearTick);
        return m_currentTick >= m_clearTick;
    }

}
