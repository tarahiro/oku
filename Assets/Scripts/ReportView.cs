using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReportView : MonoBehaviour
{
    [SerializeField] TextMeshPro m_nameText;
    [SerializeField] TextMeshPro m_deadLineText;
    [SerializeField] TextMeshPro m_progressText;
    public PostitPhysics postitPhysics;

    //�F�͕tⳂ̍쐬���@���ł܂��Ă���ēx����
    //[SerializeField] Image m_colorImage;
    //�Q�[�W�͈�U�폜�BPC��ʂ����Ă�͂�K�v������������ēx�ǉ����邩��
    //[SerializeField] Image m_gauge;
   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeReport(string reportName, DateTime deadLine,int colorId)
    {
        SetNameText(reportName);
        SetDeadLineText(deadLine);
        SetProgress(0f);
    }

    public void UpdateReportView(int currentTick, int clearTick)
    {
        SetProgress((float)currentTick / clearTick);
    }

    public void Clear()
    {
        Destroy(gameObject);
    }



    void SetNameText(string reportName)
    {
        m_nameText.text = reportName;

    }

    void SetDeadLineText(DateTime deadLine)
    {
        m_deadLineText.text = deadLine.ToString("yyyy/MM/dd") + "�Y";

    }

    void SetColor(Color color)
    {
        //�F�͕tⳂ̍쐬���@���ł܂��Ă���ēx����
        //m_colorImage.color = color;
    }

    void SetProgress(float progress)
    {
        m_progressText.text = "�i��" + ((int)(progress * 100)).ToString() + "%";
        //m_gauge.fillAmount = progress;
    }


}
