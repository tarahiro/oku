using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ReportController : MonoBehaviour
{

    List<TimeSpan> c_timeSpanOfReportEfficiency = new List<TimeSpan>()
    {
        new TimeSpan(1,0,0,0),
        new TimeSpan(3,0,0,0)
    };

    List<int> c_reportEfficiency = new List<int>()
    {
        1,2,3
    };

    [SerializeField] MainManager m_mainManager;
    [SerializeField] ReportControllerView m_view;
    [SerializeField] LevelView m_levelView;
    [SerializeField] PcDisplayView m_pcDisplayView;
    [SerializeField] Mental m_mental;

    List<Report> m_reportList;
    int m_currentLevel = 0;
    int m_currentActiveReportIndex = -1;

    bool m_isExausted = false;

    private void Awake()
    {
        m_reportList = new List<Report>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ProgressReport(int ProgressTickCount)
    {
        //レベル判定
        SetEfficiencyLevel(JudgeLevel());

        if (m_reportList[m_currentActiveReportIndex].IsClearReport(ProgressTickCount * c_reportEfficiency[m_currentLevel]))
        {
            FinishReport();
        }

        //メンタル消費
        m_mental.ComsumpMental(ProgressTickCount);

    }

    public void CheckDeadLine()
    {
        //レポート締め切り超過判定
        for (int i = 0; i < m_reportList.Count; i++)
        {
            if (StaticVariableCollector.gameTime > m_reportList[i].calculateDeadLine)
            {
                m_mainManager.GameOver();
            }
        }
    }



    public void AddReport(ReportMasterDataList.ReportMasterData reportData)
    {
        m_reportList.Add(gameObject.AddComponent<Report>());
        m_reportList[m_reportList.Count - 1].SetReport(reportData.name, reportData.GetDeadLine(), reportData.clearTick, reportData.colorId,
            m_view,m_pcDisplayView);
        TryStartNewReport();
    }

    public void SwitchReport(int movingIndex,int standbyIndex)
    {
        Report t_report = m_reportList[movingIndex];
        m_reportList[movingIndex] = m_reportList[standbyIndex];
        m_reportList[standbyIndex] = t_report;

        m_view.SwitchReport(movingIndex, standbyIndex);

        if (m_currentActiveReportIndex == movingIndex)
        {
            m_currentActiveReportIndex = standbyIndex;
        }
        else if (m_currentActiveReportIndex == standbyIndex)
        {
            m_currentActiveReportIndex = movingIndex;
        }

        TryStartNewReport();
    }

    public void ForceAllReportClear()
    {
        while(m_reportList.Any(x => !x.isFinished))
        {
            FinishReport();
            TryStartNewReport();
        }
        while(m_reportList.Count > 0)
        {
            ClearReport();
        }
    }

    void FinishReport()
    {
        m_reportList[m_currentActiveReportIndex].Finish();
        m_currentActiveReportIndex = -1;
        m_levelView.SetReportEfficiencyLevel(0);
        m_mainManager.JudgeState();
    }

    public void ClearReport()
    {
        for (int i = m_reportList.Count - 1; i >= 0; i--)
        {
            if (m_reportList[i].isFinished)
            {
                m_reportList[i].Clear();
                m_reportList.RemoveAt(i);
                m_view.Clear(i);
                if(m_currentActiveReportIndex > i) m_currentActiveReportIndex--;
            }
        }
    }

    int JudgeLevel()
    {
        //サボり中ならレベル0
        if (StaticVariableCollector.mainState == MainManager.MainState.Savotage) return 0;

        //スタックされたレポートがなかったらレベル0
        if (m_currentActiveReportIndex < 0) return 0;

        //作成中レポートの締め切りと今の時間を照合してレベル判定
        for (int i = 0; i < c_timeSpanOfReportEfficiency.Count; i++)
        {
            if (m_reportList[m_currentActiveReportIndex].calculateDeadLine - StaticVariableCollector.gameTime< c_timeSpanOfReportEfficiency[i])
            {
                return c_timeSpanOfReportEfficiency.Count - i;
            }
        }
        return 0;
    }


    void SetEfficiencyLevel(int level)
    {
        m_currentLevel = level;
        if (!m_isExausted)
        {
            m_levelView.SetReportEfficiencyLevel(m_currentLevel);
        }
    }

    public bool TryStartNewReport()
    {
        int maxIndex = m_currentActiveReportIndex < 0 ? m_reportList.Count : m_currentActiveReportIndex;
        for (int i = 0; i < maxIndex; i++)
        {
            if (!m_reportList[i].isFinished)
            {
                m_currentActiveReportIndex = i;
                m_reportList[m_currentActiveReportIndex].StartReport();
                SetEfficiencyLevel(JudgeLevel());
                return true;
            }
        }
        return false;
    }

    public bool TryStartReport()
    {
        for(int i = 0; i < m_reportList.Count; i++)
        {
            if (!m_reportList[i].isFinished)
            {
                m_currentActiveReportIndex = i;
                m_reportList[m_currentActiveReportIndex].StartReport();
                SetEfficiencyLevel(JudgeLevel());
                return true;
            }
        }
        return false;
    }

}
