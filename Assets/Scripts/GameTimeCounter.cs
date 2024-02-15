using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeCounter
{
    //�Q�[�����E��1����(=3600�b)�P�ʂœ���
    public const int TickSecond = 3600;

    // �Q�[�����E��1�b = �������E��1�b * TImeRatio
    public const float TimeRatio = 36000f;
    
    float m_residueTime = 0;


    public int CheckGameTimeUpdate(float time)
    {
        m_residueTime += time * TimeRatio;

        if (m_residueTime > TickSecond)
        {
            int tickCount = (int)m_residueTime / TickSecond;
            m_residueTime -= TickSecond * tickCount;

            return tickCount;
        }
        else
        {
            return 0;
        }

    }

    public int ForceProgress(TimeSpan t)
    {
        int progressTickCount = (int)t.TotalSeconds / TickSecond;
        m_residueTime = 0;
        return progressTickCount;

    }
}
