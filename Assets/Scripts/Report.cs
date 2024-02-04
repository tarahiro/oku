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
    Color m_color;

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

    public void SetReport(string reportName, DateTime deadLine, int clearTick, Color color,
        ReportControllerView reportControllerView, PcDisplayView pcDisplayView)
    {
        m_reportName = reportName;
        m_deadLine = deadLine;
        calculateDeadLine = m_deadLine + new TimeSpan(1,0,0,0);
        m_clearTick = clearTick;
        m_color = color;
        reportControllerViewCache = reportControllerView;
        pcDisplayViewCache = pcDisplayView;

        reportViewCache = reportControllerView.AddReport(m_reportName, m_deadLine, m_color);
    }

    public void Clear()
    {
        reportViewCache.Clear();
        Destroy(this);
    }

    public bool IsClearReport(DateTime NowGameTime, int ProgressTickCount)
    {
        m_currentTick += ProgressTickCount;
        pcDisplayViewCache.SetReport(m_reportName, m_currentTick, m_clearTick);
        reportViewCache.UpdateReportView(m_currentTick, m_clearTick);
        return m_currentTick >= m_clearTick;
    }

}
