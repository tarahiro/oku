using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PcDisplayView : MonoBehaviour
{
   const int c_reportProgressRow = 20;
   const int c_reportProgressColumn = 25;

    [SerializeField] TextMeshProUGUI m_actionText;
    [SerializeField] TextMeshProUGUI m_reportName;
    [SerializeField] TextMeshProUGUI m_reportProgress;
    [SerializeField] TextMeshProUGUI m_reportEfficiencyText;
    [SerializeField] GameObject m_reportObject;
    [SerializeField] GameObject m_restObject;
    float m_reportEfficiencyTextDefaultSize;
    int m_reportEfficiencyLevel;
    PcDisplayViewState m_currentState;

    // Start is called before the first frame update
    void Start()
    {
        m_currentState = PcDisplayViewState.None;
        m_reportEfficiencyTextDefaultSize = m_reportEfficiencyText.fontSize;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetReport(string reportName, int currentTick, int clearTick)
    {
        if (m_currentState != PcDisplayViewState.Report)
        {
            ResetState(PcDisplayViewState.Report);
            m_reportObject.SetActive(true);
        }

        if (m_reportName.text != reportName)
        {
            m_actionText.text = reportName + " 作業中...";
            m_reportName.text = reportName;
        }

        //ここはクラス分けた方が後々いいかも
        string s = "";
        int reportProgressTextCount = c_reportProgressColumn * c_reportProgressRow * currentTick / clearTick;
        int reportProgressColumn = reportProgressTextCount / c_reportProgressRow;
        int reportProgressRow = reportProgressTextCount % c_reportProgressColumn;

        for (int i = 0; i < reportProgressColumn; i++)
        {
            for (int j = 0; j < c_reportProgressRow; j++)
            {
                s += "〓";
            }
            s += "\n";
        }
        for (int j = 0; j < reportProgressRow; j++)
        {
            s += "〓";
        }

        m_reportProgress.text = s;
    }

    public void Exaust(bool isExausted)
    {
        if (m_currentState != PcDisplayViewState.Exaust)
        {
            ResetState(PcDisplayViewState.Exaust);
            m_actionText.text = "つかれはてて　うごけない！";
        }
    }

    public void Savotage(bool isSavotage)
    {
        if (m_currentState != PcDisplayViewState.Savotage)
        {
            ResetState(PcDisplayViewState.Savotage);
            m_restObject.SetActive(true);
            m_actionText.text = "サボり中...";
        }
    }

    public void Rest()
    {
        if (m_currentState != PcDisplayViewState.Rest)
        {
            ResetState(PcDisplayViewState.Rest);
            m_restObject.SetActive(true);
            m_actionText.text = "休憩中...";
        }
    }

    public void ClearReport()
    {
        //レポート終了したら休憩と同義

        if (m_currentState != PcDisplayViewState.Rest)
        {
            ResetState(PcDisplayViewState.Rest);
            m_restObject.SetActive(true);
            m_actionText.text = "";
        }
    }

    public void SetReportEfficiencyLevel(int level)
    {
        m_reportEfficiencyLevel = level;
        switch (m_reportEfficiencyLevel)
        {
            default:
            case 0:
                m_reportEfficiencyText.text = "";
                break;

            case 1:
                m_reportEfficiencyText.text = "締め切り間近で\nレポート効率2倍！";
                break;

            case 2:
                m_reportEfficiencyText.text = "締め切り超間近で\nレポート効率3倍！";
                m_reportEfficiencyText.fontSize = m_reportEfficiencyTextDefaultSize * 1.5f;
                break;

        }
    }

    enum PcDisplayViewState
    {
        None = 0,
        Report,
        Exaust,
        Savotage,
        Rest,
    }

    void ResetState(PcDisplayViewState pcDisplayViewState)
    {
        m_restObject.SetActive(false);
        m_reportObject.SetActive(false);
        m_currentState = pcDisplayViewState;
    }
}
