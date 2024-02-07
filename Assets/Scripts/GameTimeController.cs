using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    [SerializeField] GameTimeCounter m_counter;

    DateTime StartDateTime = new DateTime(2024, 4, 1, 10, 0, 0);

    MainManager m_mainManagerCache;
    ReportFactory m_reportFactory;
    Mental m_mental;
    PlayerInputReciever m_playerInputReciever;
    PlayerInputExecuter m_playerInputExecuter;
    [SerializeField] GameTimeView m_gameTimeView;
    [SerializeField] ReportController reportController;

    private void Awake()
    {
        m_mainManagerCache = FindObjectOfType<MainManager>();
        m_reportFactory = FindObjectOfType<ReportFactory>();
        m_mental = FindObjectOfType<Mental>();
        m_playerInputReciever = FindObjectOfType<PlayerInputReciever>();
        m_playerInputExecuter = FindObjectOfType<PlayerInputExecuter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_counter.Set(StartDateTime,this);
    }

    public void UpdateDateTime(int ProgressTickCount)
    {
        //インプットを取得
        m_playerInputReciever.RecieveKeyInput();

        //インプット処理
        m_playerInputExecuter.InputExecute();

        //ステートによらない共通処理
        UpdateCommonGameTime();


        //ステート依存の処理
        switch (StaticVariableCollector.mainState)
        {
            case MainManager.MainState.None:
                m_mainManagerCache.JudgeState();
                break;

            case MainManager.MainState.Rest:
                m_mental.RestoreMental(ProgressTickCount);
                m_mainManagerCache.JudgeState();
                break;

            case MainManager.MainState.Report:
                reportController.ProgressReport(ProgressTickCount);
                break;

            case MainManager.MainState.Savotage:
                m_mental.RestoreMental(ProgressTickCount);
                break;

            case MainManager.MainState.Exhausted:
                m_mental.RestoreExhaust(ProgressTickCount);
                break;


        }

        CheckSituation();
    }

    void UpdateCommonGameTime()
    {
        m_gameTimeView.UpdateTime(StaticVariableCollector.gameTime);
        m_reportFactory.ReportDataCheck();
    }

    void CheckSituation()
    {
        reportController.CheckDeadLine();
    }
  
}
