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

    List<ReportData> m_fakeReportDataList = new List<ReportData>() {
        new ReportData("現代物理学 レポート",new DateTime(2024,4,2,0,0,0), new DateTime(2024, 4, 5),80, Color.green),
        new ReportData("統計力学 レポート",new DateTime(2024,4,7,0,0,0), new DateTime(2024, 4, 17),200, Color.yellow),
        new ReportData("力学A レポート",new DateTime(2024,4,9,0,0,0), new DateTime(2024, 4, 20),80, Color.green),
        new ReportData("電磁気学A レポート",new DateTime(2024,4,11,0,0,0), new DateTime(2024, 4, 20),80, Color.green),
    };

    [SerializeField] MainManager m_mainManager;
    [SerializeField] ReportControllerView m_view;
    [SerializeField] PcDisplayView m_pcDisplayView;
    [SerializeField] Mental m_mental;

    List<Report> m_reportList;
    int m_currentLevel = 0;

    bool m_isSavotage = false;
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

    public void UpateGameTime(DateTime NowGameTime, int ProgressTickCount)
    {
        //暫定でレポート追加をここで記載、将来的に別クラスに分ける

        for (int i = m_fakeReportDataList.Count - 1; i >= 0; i--)
        {
            if (NowGameTime > m_fakeReportDataList[i].startDate)
            {
                AddReport(m_fakeReportDataList[i]);
                m_fakeReportDataList.RemoveAt(i);
            }
        }


        //レベル判定
        SetEfficiencyLevel(JudgeLevel(NowGameTime));

        //レポート進行
        if (!m_isSavotage && !m_isExausted)
        {

            if (m_reportList.Count > 0)
            {

                if (m_reportList[0].IsClearReport(NowGameTime, ProgressTickCount * c_reportEfficiency[m_currentLevel]))
                {
                    ClearReport();
                }

                //メンタル消費
                m_mental.ComsumpMental(ProgressTickCount);
            }
            else
            {
                //メンタル回復
                m_mental.RestoreMental(ProgressTickCount);
                m_pcDisplayView.Rest();
            }
        }
        else
        {
            //メンタル回復
            m_mental.RestoreMental(ProgressTickCount);
        }

        //レポート締め切り超過判定
        for (int i = 0; i < m_reportList.Count; i++)
        {
            if (NowGameTime > m_reportList[i].calculateDeadLine)
            {
                m_mainManager.GameOver();
            }
        }
    }

    public void SetSavotage(bool isSavotage)
    {
        m_isSavotage = isSavotage;
    }

    public void SetExaust(bool isExausted)
    {
        m_isExausted = isExausted;
    }

    void AddReport(ReportData reportData)
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
    }

    int JudgeLevel(DateTime NowGameTime)
    {
        //サボり中ならレベル0
        if (m_isSavotage) return 0;

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
