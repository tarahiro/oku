using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MailView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_mailTitle;
    [SerializeField] TextMeshProUGUI m_mailButton;
    [SerializeField] GameObject m_waitMailObject;
    [SerializeField] GameObject m_sendMailObject;

    const float c_sendMailTime = 1.3f;
    float m_currentTime = 0;

    private void Update()
    {
    }

    public void SetMail(string reportName)
    {
        m_currentTime = 0;
        m_mailTitle.text = reportName + "\n完了！";
        m_mailButton.text = "Space:提出";
        m_waitMailObject.SetActive(true);
        m_sendMailObject.SetActive(false);
    }

    public void SendMail()
    {
        m_waitMailObject.SetActive(false);
        m_sendMailObject.SetActive(true);
        m_mailButton.text = "提出完了！";
    }

    public bool IsEndMail()
    {
        m_currentTime += Time.deltaTime;
        return m_currentTime >= c_sendMailTime;
    }
}
