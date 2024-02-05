using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ReportController;

public class ReportFactory : MonoBehaviour
{
    MainManager m_mainManagerCache;
    ReportController m_reportControllerCache;

    List<ReportData> m_fakeReportDataList = new List<ReportData>() {
        new ReportData("現代物理学 レポート",new DateTime(2024,4,2,0,0,0), new DateTime(2024, 4, 5),80, Color.green),
        new ReportData("統計力学 レポート",new DateTime(2024,4,7,0,0,0), new DateTime(2024, 4, 17),200, Color.yellow),
        new ReportData("力学A レポート",new DateTime(2024,4,9,0,0,0), new DateTime(2024, 4, 20),80, Color.green),
        new ReportData("電磁気学A レポート",new DateTime(2024,4,11,0,0,0), new DateTime(2024, 4, 20),80, Color.green),
    };

    private void Awake()
    {
        m_mainManagerCache = FindObjectOfType<MainManager>();
        m_reportControllerCache = FindObjectOfType<ReportController>(); 
    }

    public void ReportDataCheck()
    {

        for (int i = m_fakeReportDataList.Count - 1; i >= 0; i--)
        {
            if (m_mainManagerCache.gameTime > m_fakeReportDataList[i].startDate)
            {
                m_reportControllerCache.AddReport(m_fakeReportDataList[i]);
                m_fakeReportDataList.RemoveAt(i);
            }
        }
    }
}
