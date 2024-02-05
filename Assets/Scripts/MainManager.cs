using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    [SerializeField] GameManager m_GameManger;
    [SerializeField]ReportController m_reportController;
    [SerializeField]PlayerControllerView m_playerControllerView;
    [SerializeField] PcDisplayView m_pcDisplayView;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StaticVariableCollector.SetMainState(MainState.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameTime(DateTime t_gameTime)
    {
        StaticVariableCollector.SetGameTime(t_gameTime);
    }

    public void Exaust()
    {
        StaticVariableCollector.SetMainState(MainState.Exhausted);
        m_playerControllerView.Exaust();
        m_pcDisplayView.Exaust();
    }

    public void RemoveExaust()
    {
        JudgeState();
    }

    public void GameOver()
    {
        m_GameManger.GameOver();
    }

    public void Savotage()
    {
        StaticVariableCollector.SetMainState(MainState.Savotage);
        m_playerControllerView.Savotage();
        m_pcDisplayView.Savotage();
    }

    public void RemoveSavotage()
    {
        JudgeState();
    }

    public void JudgeState()
    {
        //レポートがあるかないかを判定。あるならReport、ないならRestへステート遷移
        if (m_reportController.IsReportExist())
        {
            StartReport();
        }
        else
        {
            if (StaticVariableCollector.mainState != MainState.Rest)
            {
                Rest();
            }
        }

    }

    public void StartReport()
    {
        StaticVariableCollector.SetMainState(MainState.Report);
        m_reportController.StartReport();
        m_playerControllerView.StartReport();
    }

    public void ClearReport()
    {
        JudgeState();
    }

    public void Rest()
    {
        StaticVariableCollector.SetMainState(MainState.Rest);
        m_pcDisplayView.Rest();
    }

    public enum MainState
    {
        None,
        Report,
        Savotage,
        Exhausted,
        Rest
    }

}
