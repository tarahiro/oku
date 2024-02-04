using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    MainState m_mainState;
    [SerializeField] GameManager m_GameManger;
    [SerializeField]ReportController m_reportController;
    [SerializeField]PlayerControllerView m_playerControllerView;
    [SerializeField] PcDisplayView m_pcDisplayView;

    bool m_isExausted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        m_mainState = MainState.Normal;
        m_isExausted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool SetSavotage(bool isSavotage)
    {
        //何らかの理由でサボタージュ判定が変化できなかったらfalseを返す
        if (m_isExausted)
        {
            return false;
        }
        else
        {
            m_reportController.SetSavotage(isSavotage);
            m_playerControllerView.SetSavotage(isSavotage);
            m_pcDisplayView.Savotage(isSavotage);
            return true;
        }
    }

    public void SetExaust(bool isExaust)
    {
        m_isExausted = isExaust;
        m_reportController.SetExaust(m_isExausted);
        m_playerControllerView.SetExaust(m_isExausted);
        m_pcDisplayView.Exaust(isExaust);
    }

    public void GameOver()
    {
        m_GameManger.GameOver();
    }

    enum MainState
    {
        None,
        Normal,
    }
}
