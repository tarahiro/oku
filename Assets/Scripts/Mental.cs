using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mental : MonoBehaviour
{
    const float c_tickToConsumpMentalRatio = 1 / 120f;
    const float c_tickToRestoreMentalRatio = 1 / 45f;
    const float c_tickToRestoreExhaustRatio = 1 / 60f;

    [SerializeField] MentalView m_view;
    [SerializeField] MainManager mainManagerCache;

    float m_mental;
    bool m_isExausted;
    float m_exaustGauge;

    private void OnEnable()
    {
        SetMental(0.75f,false);
        m_isExausted = false;
    }

    public void ComsumpMental(int ProgressTickCount)
    {
        SetMental(m_mental - ProgressTickCount * c_tickToConsumpMentalRatio, true);
    }

    public void RestoreMental(int ProgressTickCount)
    {
        SetMental(m_mental + ProgressTickCount * c_tickToRestoreMentalRatio, false);
    }
    public void RestoreExhaust(int ProgressTickCount)
    {
        m_exaustGauge -= ProgressTickCount * c_tickToRestoreExhaustRatio;
        if (m_exaustGauge < 0)
        {
            Revival();
        }
        m_view.RestoreExaust(m_exaustGauge);
    }

    void SetMental(float mental, bool isConsump)
    {
        m_mental = mental;

        if (isConsump)
        {
            if (m_mental < 0)
            {
                m_mental = 0;
                Exhaust();
            }
            m_view.ConsumpMental(m_mental);
        }
        else
        {
            m_mental = mental;

            if (m_mental > 1)
            {
                //âΩÇÁÇ©ÇÃèàóùÇ™ïKóv
                m_mental = 1;
            }
            m_view.RestoreMental(m_mental);
        }
    }

    void Exhaust()
    {
        m_exaustGauge = 1;
        m_view.Exaust(m_exaustGauge);
        mainManagerCache.Exaust();
    }

    void Revival()
    {
        m_exaustGauge = 0;
        m_view.Revival();
        mainManagerCache.RemoveExaust();
        SetMental(0.5f, false);
    }

    
}
