using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputReciever : MonoBehaviour
{
    MainManager m_mainManagerCache;
    PlayerControllerView m_controllerView;

    private void OnEnable()
    {
        m_mainManagerCache = GameObject.FindObjectOfType<MainManager>();
        //SetSavotage(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (StaticVariableCollector.mainState != MainManager.MainState.Savotage)
            {
                Savotage();
            }
        }
        else
        {
            if(StaticVariableCollector.mainState == MainManager.MainState.Savotage)
            {
                RemoveSavotage();
            }
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
