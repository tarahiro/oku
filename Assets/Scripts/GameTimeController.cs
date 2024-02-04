using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    [SerializeField] GameTimeCounter m_counter;

    DateTime StartDateTime = new DateTime(2024, 4, 1, 10, 0, 0);

    [SerializeField] GameTimeView m_gameTimeView;
    [SerializeField] ReportController reportController;

    // Start is called before the first frame update
    void Start()
    {
        m_counter.Set(StartDateTime,this);
    }

    public void UpdateDateTime(DateTime NowGameTime, int ProgressTickCount)
    {
        m_gameTimeView.UpdateTime(NowGameTime);
        reportController.UpateGameTime(NowGameTime, ProgressTickCount);
    }
  
}
