using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_reportEfficiencyText;
    float m_reportEfficiencyTextDefaultSize;

    private void Start()
    {
        m_reportEfficiencyTextDefaultSize = m_reportEfficiencyText.fontSize;
    }
    public void SetReportEfficiencyLevel(int level)
    {
        switch (level)
        {
            default:
            case 0:
                m_reportEfficiencyText.text = "";
                break;

            case 1:
                m_reportEfficiencyText.text = "締め切り間近でレポート効率2倍！";
                break;

            case 2:
                m_reportEfficiencyText.text = "締め切り超間近でレポート効率3倍！";
                m_reportEfficiencyText.fontSize = m_reportEfficiencyTextDefaultSize * 1.5f;
                break;

        }
    }
}
