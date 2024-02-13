using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ReportMasterDataList;

public class ReportMasterDataList : MasterDataList<ReportMasterData>
{

    [Serializable]
    public class ReportMasterData
    {
        [SerializeField] public string name;
        [SerializeField] string m_startDate;
        [SerializeField] string m_deadLine;
        [SerializeField] public int clearTick;
        [SerializeField] public int colorId;

        public ReportMasterData(string name, string startDate, string deadLine, int clearTick, int colorId)
        {
            this.name = name;
            m_startDate = startDate;
            m_deadLine = deadLine;
            this.clearTick = clearTick;
            this.colorId = colorId;
        }

        public ReportMasterData Copy()
        {
            return new ReportMasterData(this.name, m_startDate, m_deadLine, this.clearTick, this.colorId);
        }

        public DateTime GetStartDate()
        {
            return DateTime.Parse(m_startDate);
        }
        public DateTime GetDeadLine()
        {
            return DateTime.Parse(m_deadLine);
        }
    }
}
