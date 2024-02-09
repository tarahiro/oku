using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReportControllerView : MonoBehaviour
{
    [SerializeField] ReportView reportViewPrefab;

   public List<ReportView> reportViewList;
    PostitPhysicsController m_postitPhysicsController;

    private void Awake()
    {
        reportViewList = new List<ReportView>();
        m_postitPhysicsController = FindObjectOfType<PostitPhysicsController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    public ReportView AddReport(string reportName, DateTime deadLine, Color color)
    {
        reportViewList.Add(Instantiate(reportViewPrefab, transform));
        reportViewList[reportViewList.Count - 1].InitializeReport(reportName, deadLine, color);
        m_postitPhysicsController.SetPostitPosition(reportViewList.Count - 1);
        return reportViewList[reportViewList.Count - 1];
    }

    public void SwitchReport(int movingIndex, int standbyIndex)
    {
        ReportView t_reportView = reportViewList[movingIndex];
        reportViewList[movingIndex] = reportViewList[standbyIndex];
        reportViewList[standbyIndex] = t_reportView;

        m_postitPhysicsController.SetPostitPosition(movingIndex);
    }


    public void Clear()
    {
        reportViewList.RemoveAt(0);
    }
}
