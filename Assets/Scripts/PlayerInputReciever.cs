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
            if (m_mainManagerCache.mainState != MainManager.MainState.Savotage)
            {
                Savotage();
            }
        }
        /*
        else
        {
            if (m_isSavotage)
            {
               SetSavotage(false);
            }

        }
        */
    }

    void Savotage()
    {
        m_mainManagerCache.Savotage();
    }

}
