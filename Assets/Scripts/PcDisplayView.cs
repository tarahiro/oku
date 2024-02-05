using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PcDisplayView : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI m_actionText;
    [SerializeField] TextMeshProUGUI m_reportEfficiencyText;
    [SerializeField] WordView m_wordView;
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

    public void StartReport(string reportName, int currentTick, int clearTick)
    {
            ResetState(PcDisplayViewState.Report);
            m_wordView.gameObject.SetActive(true);

        m_wordView.StartReport(reportName, currentTick, clearTick);

    }

    public void ProgressReport(int currentTick, int clearTick)
    {
        m_wordView.ProgressReport(currentTick, clearTick);
    }

    public void Exaust(bool isExausted)
    {
        if (m_currentState != PcDisplayViewState.Exaust)
        {
            ResetState(PcDisplayViewState.Exaust);
            m_actionText.text = "つかれはてて　うごけない！";
        }
    }

    public void Savotage()
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
        ReportClear,
        Exaust,
        Savotage,
        Rest,
    }

    void ResetState(PcDisplayViewState pcDisplayViewState)
    {
        m_restObject.SetActive(false);
        m_wordView.gameObject.SetActive(false);
        m_currentState = pcDisplayViewState;
    }
    void EllegalStateInput()
    {
        Debug.LogError("不正なステート入力です");
    }
}
