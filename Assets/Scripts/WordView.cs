using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordView : MonoBehaviour
{
    const int c_reportProgressRow = 20;
    const int c_reportProgressColumn = 25;

    [SerializeField] TextMeshProUGUI m_reportName;
    [SerializeField] TextMeshProUGUI m_reportProgress;

    public void StartReport(string reportName, int currentTick, int clearTick)
    {
        m_reportName.text = reportName;

        ProgressReport(currentTick, clearTick);
    }

    public void ProgressReport(int currentTick, int clearTick)
    {
        string s = "";
        int reportProgressTextCount = c_reportProgressColumn * c_reportProgressRow * currentTick / clearTick;
        int reportProgressColumn = reportProgressTextCount / c_reportProgressRow;
        int reportProgressRow = reportProgressTextCount % c_reportProgressRow;

        for (int i = 0; i < reportProgressColumn; i++)
        {
            for (int j = 0; j < c_reportProgressRow; j++)
            {
                s += "¬";
            }
            s += "\n";
        }
        for (int j = 0; j < reportProgressRow; j++)
        {
            s += "¬";
        }

        m_reportProgress.text = s;
    }
}
