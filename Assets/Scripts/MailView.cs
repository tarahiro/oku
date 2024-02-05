using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MailView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_mailTitle;

    const float c_sendMailTime = 1.3f;
    float m_currentTime = 0;

    private void Update()
    {
    }

    public void SetMail(string reportName)
    {
        m_currentTime = 0;
        m_mailTitle.text = reportName + "\nŠ®—¹I";
    }

    public bool IsEndMail()
    {
        m_currentTime += Time.deltaTime;
        return m_currentTime >= c_sendMailTime;
    }
}
