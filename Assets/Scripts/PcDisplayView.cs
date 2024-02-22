using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PcDisplayView : MonoBehaviour
{

    [SerializeField] WordView m_wordView;
    [SerializeField] MailView m_mailView;
    [SerializeField] GameObject m_restObject;
    [SerializeField] CinemachineImpulseSource m_impulseSource;
    [SerializeField] EffectHandler m_reportClearEffectHandler;
    PcDisplayViewState m_currentState;

    bool m_isMailing = false;

    // Start is called before the first frame update
    void Start()
    {
        m_currentState = PcDisplayViewState.None;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isMailing)
        {
            if (m_mailView.IsEndMail())
            {
                EndMail();
            }
        }
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

    public void Exaust()
    {
            ResetState(PcDisplayViewState.Exaust);
    }

    public void Savotage()
    {
            ResetState(PcDisplayViewState.Savotage);
            m_restObject.SetActive(true);
    }

    public void Rest()
    {
        ResetState(PcDisplayViewState.Rest);
        m_restObject.SetActive(true);
    }

    public void FinishReport(string reportName)
    {
        m_mailView.gameObject.SetActive(true);
        m_mailView.SetMail(reportName);

    }

    public void ClearReport()
    {
        m_reportClearEffectHandler.CallEffect();
        m_mailView.SendMail();
        m_impulseSource.GenerateImpulse();
        m_isMailing = true;
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
        if (m_currentState == pcDisplayViewState) EllegalStateInput();
        m_restObject.SetActive(false);
        m_wordView.gameObject.SetActive(false);
        m_currentState = pcDisplayViewState;
    }

    void EndMail()
    {
        m_mailView.gameObject.SetActive(false);
        m_isMailing = false;
    }

    void EllegalStateInput()
    {
        Debug.LogError("不正なステート入力です");
    }
}
