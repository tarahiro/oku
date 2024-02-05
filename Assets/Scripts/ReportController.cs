using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] PcDisplayView m_pcDisplayView;
    [SerializeField] Mental m_mental;

    List<Report> m_reportList;
    int m_currentLevel = 0;

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
        SetEfficiencyLevel(JudgeLevel(StaticVariableCollector.gameTime));

        if (m_reportList[0].IsClearReport(StaticVariableCollector.gameTime, ProgressTickCount * c_reportEfficiency[m_currentLevel]))
        {
            ClearReport();
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

    public bool IsReportExist()
    {
        return m_reportList.Count > 0;
    }

    public void StartReport()
    {
        m_reportList[0].StartReport();
    }

    public void AddReport(ReportData reportData)
    {
        m_reportList.Add(gameObject.AddComponent<Report>());
        m_reportList[m_reportList.Count - 1].SetReport(reportData.name, reportData.deadLine, reportData.clearTick, reportData.color,
            m_view,m_pcDisplayView);
    }

    void ClearReport()
    {
        m_reportList[0].Clear();
        m_reportList.RemoveAt(0);
        m_view.Clear();
        m_pcDisplayView.ClearReport();
        m_mainManager.ClearReport();
    }

    int JudgeLevel(DateTime NowGameTime)
    {
        //サボり中ならレベル0
        if (StaticVariableCollector.mainState == MainManager.MainState.Savotage) return 0;

        //スタックされたレポートがなかったらレベル0
        if (m_reportList.Count == 0) return 0;

        //作成中レポートの締め切りと今の時間を照合してレベル判定
        for (int i = 0; i < c_timeSpanOfReportEfficiency.Count; i++)
        {
            if (m_reportList[0].calculateDeadLine - NowGameTime < c_timeSpanOfReportEfficiency[i])
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
            m_pcDisplayView.SetReportEfficiencyLevel(m_currentLevel);
        }
    }



    [System.Serializable]
    public class ReportData
    {
        public string name { get; private set; }
        public DateTime startDate { get; private set; }
        public DateTime deadLine { get; private set; }
        public int clearTick { get; private set; }
        public Color color { get; private set; }

        public ReportData(string name, DateTime startDate, DateTime deadLine, int clearTick, Color color)
        {
            this.name = name;
            this.startDate = startDate;
            this.deadLine = deadLine;
            this.clearTick = clearTick;
            this.color = color;
        }
    }
}
