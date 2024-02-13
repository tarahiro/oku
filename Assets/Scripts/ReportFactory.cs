using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ReportController;

public class ReportFactory : MonoBehaviour
{
    ReportController m_reportControllerCache;

    ReportMasterDataList m_reportDataList;
    List<ReportData> m_reportData;

    private void Awake()
    {
        m_reportControllerCache = FindObjectOfType<ReportController>();
        m_reportData = new List<ReportData>();

        m_reportDataList = StaticVariableCollector.GetReportMasterDataList();
        for (int i = m_reportDataList.Count - 1; i >= 0; i--)
        {
            m_reportData.Add( new ReportData(m_reportDataList.TryGetFromIndex(i)));
        }
    }

    public void ReportDataCheck()
    {

        for (int i = m_reportData.Count - 1; i >= 0; i--)
        {
            if (!m_reportData[i].isSet)
            {
                if (StaticVariableCollector.gameTime > m_reportData[i].reportMasterData.GetStartDate())
                {
                    m_reportControllerCache.AddReport(m_reportData[i].reportMasterData);
                    m_reportData[i].Set();
                }
            }
        }
    }

    public class ReportData
    {
        public ReportMasterDataList.ReportMasterData reportMasterData { get; private set; }
        public bool isSet { get; private set; } = false;

        public ReportData(ReportMasterDataList.ReportMasterData reportMasterData)
        {
            this.reportMasterData = reportMasterData.Copy();
        }

        public void Set()
        {
            isSet = true;
        }
    }
}
