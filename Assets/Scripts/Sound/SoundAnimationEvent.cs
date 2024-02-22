using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAnimationEvent:MonoBehaviour
{
    public void PlaySE(SoundManager.SELabel m_SElabel)
    {
        SoundManager.PlaySE(m_SElabel);
    }
}
