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
                m_reportEfficiencyText.text = "���ߐ؂�ԋ߂Ń��|�[�g����2�{�I";
                break;

            case 2:
                m_reportEfficiencyText.text = "���ߐ؂蒴�ԋ߂Ń��|�[�g����3�{�I";
                m_reportEfficiencyText.fontSize = m_reportEfficiencyTextDefaultSize * 1.5f;
                break;

        }
    }
}
