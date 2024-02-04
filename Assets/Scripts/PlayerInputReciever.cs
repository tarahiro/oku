using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputReciever : MonoBehaviour
{
    MainManager m_mainManagerCache;
    PlayerControllerView m_controllerView;
    bool m_isSavotage;

    private void OnEnable()
    {
        m_mainManagerCache = GameObject.FindObjectOfType<MainManager>();
        SetSavotage(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if(!m_isSavotage)
            {
                SetSavotage(true);
            }
        }
        else
        {
            if (m_isSavotage)
            {
                SetSavotage(false);
            }

        }
    }

    void SetSavotage(bool isSavotage)
    {
        if (m_mainManagerCache.SetSavotage(isSavotage)) m_isSavotage = isSavotage;
    }

}
