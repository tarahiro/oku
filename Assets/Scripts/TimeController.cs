using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    MainManager m_mainManagerCache;
    PlayerInputReciever m_playerInputReciever;
    PlayerInputExecuter m_playerInputExecuter;
    GameTimeCounter m_gameTimeCounter;
    GameTimeController m_GameTimeControllerCache;
    // Start is called before the first frame update
    private void Awake()
    {
        m_mainManagerCache = FindObjectOfType<MainManager>();
        m_playerInputReciever = FindObjectOfType<PlayerInputReciever>();
        m_playerInputExecuter = FindObjectOfType<PlayerInputExecuter>(); 
        m_GameTimeControllerCache = FindObjectOfType<GameTimeController>();
        m_gameTimeCounter = new GameTimeCounter();
    }

    // Update is called once per frame
    void Update()
    {
        //インプットを取得
        m_playerInputReciever.RecieveKeyInput();

        //インプット処理
        m_playerInputExecuter.InputExecute();

        //ゲーム時間の更新判定
        CheckGameTimeUpdate(m_gameTimeCounter.CheckGameTimeUpdate(Time.deltaTime));
    }

    public void SetStartTime(DateTime time)
    {
        UpdateDateTime(time, 0);
    }

    public void GoToSpecificDay(DateTime d)
    {
        CheckGameTimeUpdate(m_gameTimeCounter.ForceProgress(d - StaticVariableCollector.gameTime));
    }

    public void GoToNextDay()
    {
        GoToSpecificDay(new DateTime(StaticVariableCollector.gameTime.Year, StaticVariableCollector.gameTime.Month, StaticVariableCollector.gameTime.Day) + TimeSpan.FromDays(1));
    }

    void CheckGameTimeUpdate(int tickCount)
    {
        if (tickCount > 0)
        {
            UpdateDateTime(new DateTime(StaticVariableCollector.gameTime.Ticks + TimeSpan.TicksPerSecond * GameTimeCounter.TickSecond * tickCount), tickCount);
        }
    }

    void UpdateDateTime(DateTime dateTime, int progressTickCount)
    {
        m_mainManagerCache.SetGameTime(dateTime);
        m_GameTimeControllerCache.UpdateDateTime(progressTickCount);
    }
}
