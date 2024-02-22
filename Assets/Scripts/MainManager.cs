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
    [SerializeField] TimeController m_TimeController;
    DateTime StartDateTime = new DateTime(2024, 4, 1, 10, 0, 0);
    
    public MainState mainState { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        m_TimeController.SetStartTime(StartDateTime);
    }

    private void OnEnable()
    {
        SetMainState(MainState.None);
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
        SetMainState(MainState.Exhausted);
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
        SetMainState(MainState.Savotage);
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
        if (m_reportController.TryStartReport())
        {
            m_playerControllerView.StartReport();
            SetMainState(MainState.Report);
        }
        else
        {
            if (StaticVariableCollector.mainState != MainState.Rest)
            {
                Rest();
            }
        }

    }


    void SetMainState(MainState t_mainState)
    {
        if (mainState == t_mainState && mainState != MainState.None)
        {
            EllegalStateInput();
        }
        mainState = t_mainState;
    }

    public void Rest()
    {
        SetMainState(MainState.Rest);
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
    void EllegalStateInput()
    {
        Debug.LogError("不正なステート入力です");
    }

}
