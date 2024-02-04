using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReportView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_nameText;
    [SerializeField] TextMeshProUGUI m_deadLineText;
    [SerializeField] TextMeshProUGUI m_progressText;
    [SerializeField] Image m_colorImage;
    [SerializeField] Image m_gauge;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeReport(string reportName, DateTime deadLine, Color color)
    {
        SetNameText(reportName);
        SetDeadLineText(deadLine);
        SetColor(color);
        SetProgress(0f);
    }

    public void UpdateReportView(int currentTick, int clearTick)
    {
        SetProgress((float)currentTick / clearTick);
    }

    public void Clear()
    {
        Destroy(gameObject); ;
    }



    void SetNameText(string reportName)
    {
        m_nameText.text = reportName;

    }

    void SetDeadLineText(DateTime deadLine)
    {
        m_deadLineText.text = deadLine.ToString("yyyy/MM/dd") + "ÅY";

    }

    void SetColor(Color color)
    {
        m_colorImage.color = color;
    }

    void SetProgress(float progress)
    {
        m_progressText.text = "êiíª" + ((int)(progress * 100)).ToString() + "%";
        m_gauge.fillAmount = progress;
    }


}
