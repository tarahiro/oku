using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInputExecuter : MonoBehaviour
{
    PlayerInputReciever m_reciever;
    MainManager m_mainManagerCache;

    private void Awake()
    {
        m_mainManagerCache = FindObjectOfType<MainManager>();
        m_reciever = GetComponent<PlayerInputReciever>();
    }

    public void InputExecute()
    {
        m_reciever.RayCastExecute();

        if (m_reciever.m_keyInKeyList.Exists(x => x == KeyCode.Z))
        {
            if (StaticVariableCollector.mainState != MainManager.MainState.Savotage) Savotage();
        }
        else
        {
            if (StaticVariableCollector.mainState == MainManager.MainState.Savotage) RemoveSavotage();
        }
    }

    void Savotage()
    {
        m_mainManagerCache.Savotage();
    }

    void RemoveSavotage()
    {
        m_mainManagerCache.RemoveSavotage();
    }

}
