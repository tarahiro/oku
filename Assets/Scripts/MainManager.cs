using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public MainState mainState { get; private set; }
    public DateTime gameTime { get; private set; }


    [SerializeField] GameManager m_GameManger;
    [SerializeField]ReportController m_reportController;
    [SerializeField]PlayerControllerView m_playerControllerView;
    [SerializeField] PcDisplayView m_pcDisplayView;

    bool m_isExausted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        mainState = MainState.Report;
        m_isExausted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameTime(DateTime t_gameTime)
    {
        gameTime = t_gameTime;
    }

    public bool SetSavotage(bool isSavotage)
    {
        //何らかの理由でサボタージュ判定が変化できなかったらfalseを返す
        if (m_isExausted)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetExaust(bool isExaust)
    {
        m_isExausted = isExaust;
        m_reportController.SetExaust(m_isExausted);
        m_playerControllerView.SetExaust(m_isExausted);
        m_pcDisplayView.Exaust(isExaust);
    }

    public void GameOver()
    {
        m_GameManger.GameOver();
    }

    public void Savotage()
    {
        if(mainState == MainState.Savotage)
        {
            EllegalStateInput();
        }
        m_playerControllerView.Savotage();
        m_pcDisplayView.Savotage();
        mainState = MainState.Savotage;
    }

    public void RemoveSavotage()
    {
        if (mainState != MainState.Savotage)
        {
            EllegalStateInput();
        }

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
            Rest();
        }

    }

    public void StartReport()
    {
        m_reportController.StartReport();
        mainState = MainState.Report;
    }

    public void Rest()
    {

    }

    public enum MainState
    {
        None,
        Report,
        Savotage,
        Exhausted,
    }

    void EllegalStateInput()
    {
        Debug.LogError("不正なステート入力です");
    }
}
