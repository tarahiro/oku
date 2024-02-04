using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportControllerView : MonoBehaviour
{
    const float merginY = 30f;

    [SerializeField] ReportView reportViewPrefab;

    List<ReportView> m_reportViewList;

    private void Awake()
    {
        m_reportViewList = new List<ReportView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ReportView AddReport(string reportName, DateTime deadLine, Color color)
    {
        m_reportViewList.Add(Instantiate(reportViewPrefab, transform));

        RectTransform addedReportTransform = m_reportViewList[m_reportViewList.Count - 1].GetComponent<RectTransform>();
        addedReportTransform.localPosition = Vector3.down * (m_reportViewList.Count - 1)*(addedReportTransform.sizeDelta.y + merginY);

        m_reportViewList[m_reportViewList.Count - 1].InitializeReport(reportName, deadLine, color);

        return m_reportViewList[m_reportViewList.Count - 1];
    }

    public void Clear()
    {
        m_reportViewList.RemoveAt(0);
    }
}
