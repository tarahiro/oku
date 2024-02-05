using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PcDisplayView : MonoBehaviour
{

    [SerializeField] WordView m_wordView;
    [SerializeField] MailView m_mailView;
    [SerializeField] GameObject m_restObject;
    PcDisplayViewState m_currentState;

    Action<int, int> StuckStartReport;
    Action StuckAction;

    // Start is called before the first frame update
    void Start()
    {
        m_currentState = PcDisplayViewState.None;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_currentState == PcDisplayViewState.Mail)
        {
            if (m_mailView.IsEndMail())
            {
                StuckAction.Invoke();
            }
        }
    }

    public void StartReport(string reportName, int currentTick, int clearTick,bool isStuck = false)
    {
        if (m_currentState == PcDisplayViewState.Mail && !isStuck)
        {
            StuckStartReport = (i, j) => StartReport(reportName, i, j,true);
            StuckAction = () => StuckStartReport(currentTick, clearTick);
        }
        else
        {
            ResetState(PcDisplayViewState.Report);
            m_wordView.gameObject.SetActive(true);
            m_wordView.StartReport(reportName, currentTick, clearTick);
        }

    }

    public void ProgressReport(int currentTick, int clearTick, bool isStuck = false)
    {
        if (m_currentState == PcDisplayViewState.Mail && !isStuck)
        {
            StuckAction = () => StuckStartReport(currentTick, clearTick);
        }
        else
        {
           m_wordView.ProgressReport(currentTick, clearTick);
        }
    }

    public void Exaust(bool isStuck = false)
    {
        if (m_currentState == PcDisplayViewState.Mail && !isStuck)
        {
            StuckAction = () => Exaust(true);
        }
        else
        {
            ResetState(PcDisplayViewState.Exaust);
        }
    }

    public void Savotage(bool isStuck = false)
    {
        if (m_currentState == PcDisplayViewState.Mail && !isStuck)
        {
            StuckAction = () => Savotage(true);
        }
        else
        {
            ResetState(PcDisplayViewState.Savotage);
            m_restObject.SetActive(true);
        }
    }

    public void Rest(bool isStuck = false)
        {
            if (m_currentState == PcDisplayViewState.Mail && !isStuck)
            {
                StuckAction = () => Rest(true);
            }
            else
            {
                ResetState(PcDisplayViewState.Rest);
                m_restObject.SetActive(true);
            }
    }

    public void ClearReport(string reportName)
    {
        //レポート終了したら休憩と同義

        if (m_currentState != PcDisplayViewState.Rest)
        {
            ResetState(PcDisplayViewState.Mail);
            m_mailView.gameObject.SetActive(true);
            m_mailView.SetMail(reportName);
        }
    }


    enum PcDisplayViewState
    {
        None = 0,
        Report,
        Mail,
        Exaust,
        Savotage,
        Rest,
    }

    void ResetState(PcDisplayViewState pcDisplayViewState)
    {
        if (m_currentState == pcDisplayViewState) EllegalStateInput();
        m_restObject.SetActive(false);
        m_wordView.gameObject.SetActive(false);
        m_mailView.gameObject.SetActive(false);
        m_currentState = pcDisplayViewState;
    }
    void EllegalStateInput()
    {
        Debug.LogError("不正なステート入力です");
    }
}
